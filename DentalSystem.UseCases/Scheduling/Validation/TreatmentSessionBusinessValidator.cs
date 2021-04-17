using System.Threading;
using System.Threading.Tasks;
using DentalSystem.Common.Helpers.Extensions;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Boundaries.UseCases.Scheduling.Dto.Input;
using FluentValidation;

namespace DentalSystem.UseCases.Scheduling.Validation
{
    public class TreatmentSessionBusinessValidator : AbstractValidator<ITreatmentSessionInput>
    {
        public TreatmentSessionBusinessValidator(
            TreatmentSessionValidator simpleValidator,
            IGenericRepository<TreatmentSession> treatmentSessionRepository)
        {
            RuleFor(m => m).SetValidator(simpleValidator);

            RuleFor(m => m)
                .MustAsync((m, ctx, ct) => HasNoOverlappingsForPatient(m, ct))
                .WithMessage("There is overlapping treatment session for patient.");

            RuleFor(m => m)
                .MustAsync((m, ctx, ct) => HasNoOverlappingsForDentalTeam(m, ct))
                .WithMessage("There is overlapping treatment session for dental team.");

            TreatmentSessionRepository = treatmentSessionRepository;
        }

        public IGenericRepository<TreatmentSession> TreatmentSessionRepository { get; }

        private async Task<bool> HasNoOverlappingsForPatient(
            ITreatmentSessionInput model,
            CancellationToken cancellationToken)
            => await TreatmentSessionRepository
                .Where(ts => ts.Patient.ReferenceId == model.PatientReferenceId)
                .NoneAsync(
                    predicate: ts => ts.Status != TreatmentSessionStatus.Rejected
                        && ts.Start <= model.End
                        && ts.End >= model.Start,
                    cancellationToken: cancellationToken
                );

        private async Task<bool> HasNoOverlappingsForDentalTeam(
            ITreatmentSessionInput model,
            CancellationToken cancellationToken)
            => await TreatmentSessionRepository
                .Where(ts => ts.DentalTeam.ReferenceId == model.DentalTeamReferenceId)
                .NoneAsync(
                    predicate: ts => ts.Status != TreatmentSessionStatus.Rejected
                        && ts.Start <= model.End
                        && ts.End >= model.Start,
                    cancellationToken: cancellationToken
                );
    }
}