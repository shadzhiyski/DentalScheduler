using System.Threading;
using System.Threading.Tasks;
using DentalSystem.Common.Helpers.Extensions;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Domain.Scheduling.Enumerations;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Microsoft.EntityFrameworkCore;

namespace DentalSystem.Application.UseCases.Scheduling.Validation
{
    public class TreatmentSessionBusinessValidator : AbstractValidator<ITreatmentSessionInput>
    {
        public const string OverlappingTreatmentSessionForPatientMessageName
            = "OverlappingTreatmentSessionForPatient";
        public const string OverlappingTreatmentSessionForDentalTeamMessageName
            = "OverlappingTreatmentSessionForDentalTeam";
        public const string InvalidDentalTeamMessageName = "InvalidDentalTeam";

        public TreatmentSessionBusinessValidator(
            IStringLocalizer<TreatmentSessionBusinessValidator> localizer,
            TreatmentSessionValidator simpleValidator,
            IReadRepository<TreatmentSession> treatmentSessionRepository,
            IReadRepository<DentalTeam> dentalTeamReadRepository)
        {
            RuleFor(m => m)
                .SetValidator(simpleValidator)
                .DependentRules(() =>
                {
                    RuleFor(m => m.DentalTeamReferenceId)
                        .MustAsync((m, ctx, ct) => HasDentalTeam(m, ct))
                        .WithMessage(localizer[InvalidDentalTeamMessageName]);

                    RuleFor(m => m.PatientReferenceId)
                        .MustAsync((m, ctx, ct) => HasNoOverlappingsForPatient(m, ct))
                        .WithMessage(localizer[OverlappingTreatmentSessionForPatientMessageName]);

                    RuleFor(m => m.DentalTeamReferenceId)
                        .MustAsync((m, ctx, ct) => HasNoOverlappingsForDentalTeam(m, ct))
                        .WithMessage(localizer[OverlappingTreatmentSessionForDentalTeamMessageName]);
                });

            TreatmentSessionRepository = treatmentSessionRepository;
            DentalTeamReadRepository = dentalTeamReadRepository;
        }

        public IReadRepository<TreatmentSession> TreatmentSessionRepository { get; }

        public IReadRepository<DentalTeam> DentalTeamReadRepository { get; }

        private Task<bool> HasDentalTeam(
            ITreatmentSessionInput model,
            CancellationToken cancellationToken)
            => DentalTeamReadRepository
                .Where(ts => ts.ReferenceId == model.DentalTeamReferenceId)
                .AnyAsync(cancellationToken);

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