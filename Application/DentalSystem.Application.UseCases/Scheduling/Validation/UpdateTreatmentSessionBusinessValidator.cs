using System.Threading;
using System.Threading.Tasks;
using DentalSystem.Common.Helpers.Extensions;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Domain.Scheduling.Enumerations;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace DentalSystem.Application.UseCases.Scheduling.Validation
{
    public class UpdateTreatmentSessionBusinessValidator : AbstractValidator<IUpdateTreatmentSessionInput>
    {
        public const string NotExistingTreatmentSessionMessageName = "NotExistingTreatmentSession";

        public UpdateTreatmentSessionBusinessValidator(
            IStringLocalizer<UpdateTreatmentSessionBusinessValidator> localizer,
            IStringLocalizer<TreatmentSessionBusinessValidator> addLocalizer,
            UpdateTreatmentSessionValidator simpleValidator,
            IReadRepository<TreatmentSession> treatmentSessionRepository)
        {
            RuleFor(m => m)
                .SetValidator(simpleValidator)
                .DependentRules(() =>
                {
                    RuleFor(m => m.ReferenceId)
                        .MustAsync((m, ctx, ct) => ExistsTreatmentSession(m, ct))
                        .WithMessage(localizer[NotExistingTreatmentSessionMessageName])
                        .DependentRules(() =>
                        {
                            RuleFor(m => m.PatientReferenceId)
                                .MustAsync((m, ctx, ct) => HasNoOverlappingsForPatient(m, ct))
                                .WithMessage(addLocalizer[TreatmentSessionBusinessValidator.OverlappingTreatmentSessionForPatientMessageName]);

                            RuleFor(m => m.DentalTeamReferenceId)
                                .MustAsync((m, ctx, ct) => HasNoOverlappingsForDentalTeam(m, ct))
                                .WithMessage(addLocalizer[TreatmentSessionBusinessValidator.OverlappingTreatmentSessionForDentalTeamMessageName]);
                        });
                });

            TreatmentSessionRepository = treatmentSessionRepository;
        }

        public IReadRepository<TreatmentSession> TreatmentSessionRepository { get; }

        private async Task<bool> ExistsTreatmentSession(
            IUpdateTreatmentSessionInput model,
            CancellationToken cancellationToken)
            => await Task.Run(() => TreatmentSessionRepository
                    .AsNoTracking()
                    .Any(
                        ts => ts.ReferenceId == model.ReferenceId
                    )
                );

        private async Task<bool> HasNoOverlappingsForPatient(
            IUpdateTreatmentSessionInput model,
            CancellationToken cancellationToken)
            => await TreatmentSessionRepository
                .Where(
                    ts => ts.ReferenceId != model.ReferenceId
                        && ts.Patient.ReferenceId == model.PatientReferenceId
                )
                .NoneAsync(
                    predicate: ts => ts.Status != TreatmentSessionStatus.Rejected
                        && ts.Start <= model.End
                        && ts.End >= model.Start,
                    cancellationToken: cancellationToken
                );

        private async Task<bool> HasNoOverlappingsForDentalTeam(
            IUpdateTreatmentSessionInput model,
            CancellationToken cancellationToken)
            => await TreatmentSessionRepository
                .Where(
                    ts => ts.ReferenceId != model.ReferenceId
                        && ts.DentalTeam.ReferenceId == model.DentalTeamReferenceId
                )
                .NoneAsync(
                    predicate: ts => ts.Status != TreatmentSessionStatus.Rejected
                        && ts.Start <= model.End
                        && ts.End >= model.Start,
                    cancellationToken: cancellationToken
                );
    }
}