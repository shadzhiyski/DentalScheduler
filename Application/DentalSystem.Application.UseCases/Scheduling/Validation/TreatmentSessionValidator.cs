using DentalSystem.Entities.Scheduling;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using FluentValidation;
using Microsoft.Extensions.Localization;
using DentalSystem.Application.UseCases.Common.Validation;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Input;

namespace DentalSystem.Application.UseCases.Scheduling.Validation
{
    public class TreatmentSessionValidator : AbstractValidator<ITreatmentSessionInput>
    {
        public const string RequiredDentalTeamMessageName = "RequiredDentalTeam";
        public const string RequiredPatientMessageName = "RequiredPatient";
        public const string RequiredTreatmentMessageName = "RequiredTreatment";
        public const string MaxDurationMessageName = "MaxDuration";
        public const string UnsupportedStatusMessageName = "UnsupportedStatus";

        public TreatmentSessionValidator(
            IStringLocalizer<TreatmentSessionValidator> localizer,
            AbstractValidator<IPeriod> periodValidator)
        {
            RuleFor(model => model.DentalTeamReferenceId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer[RequiredDentalTeamMessageName]);

            RuleFor(model => model.PatientReferenceId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer[RequiredPatientMessageName]);

            RuleFor(model => model.TreatmentReferenceId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer[RequiredTreatmentMessageName]);

            RuleFor(m => m)
                .SetValidator(periodValidator)
                .DependentRules(() =>
                {
                    RuleFor(m => m.End)
                        .LessThanOrEqualTo(model => model.Start.HasValue ? model.Start.Value.AddHours(2) : model.End.Value)
                        .WithMessage(localizer[MaxDurationMessageName]);
                });

            RuleFor(model => model.Status)
                .IsEnumName(typeof(TreatmentSessionStatus))
                .WithMessage(localizer[UnsupportedStatusMessageName]);
        }
    }
}