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
            IStringLocalizer<AddTreatmentSessionBusinessValidator> addLocalizer,
            UpdateTreatmentSessionValidator simpleValidator,
            AbstractValidator<ITreatmentSessionReferencesInput> treatmentSessionReferencesBusinessValidator,
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
                            RuleFor(m => m)
                                .SetValidator(treatmentSessionReferencesBusinessValidator);

                            RuleFor(m => m.PatientReferenceId)
                                .MustAsync((m, ctx, ct) => HasNoOverlappingsForPatient(m, ct))
                                .WithMessage(addLocalizer[AddTreatmentSessionBusinessValidator.OverlappingTreatmentSessionForPatientMessageName]);

                            RuleFor(m => m.DentalTeamReferenceId)
                                .MustAsync((m, ctx, ct) => HasNoOverlappingsForDentalTeam(m, ct))
                                .WithMessage(addLocalizer[AddTreatmentSessionBusinessValidator.OverlappingTreatmentSessionForDentalTeamMessageName]);
                        });
                });

            TreatmentSessionRepository = treatmentSessionRepository;
        }

        public IReadRepository<TreatmentSession> TreatmentSessionRepository { get; }

        private Task<bool> ExistsTreatmentSession(
            IUpdateTreatmentSessionInput model,
            CancellationToken cancellationToken)
            => Task.Run(() => TreatmentSessionRepository
                    .AsNoTracking()
                    .Any(
                        ts => ts.ReferenceId == model.ReferenceId
                    )
                );

        private Task<bool> HasNoOverlappingsForPatient(
            IUpdateTreatmentSessionInput model,
            CancellationToken cancellationToken)
            => TreatmentSessionRepository
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

        private Task<bool> HasNoOverlappingsForDentalTeam(
            IUpdateTreatmentSessionInput model,
            CancellationToken cancellationToken)
            => TreatmentSessionRepository
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