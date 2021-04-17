using System;
using System.Threading;
using System.Threading.Tasks;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DentalSystem.Application.UseCases.Scheduling.Validation
{
    public class TreatmentSessionValidator : AbstractValidator<ITreatmentSessionInput>
    {
        public const string RequiredDentalTeamMessageName = "RequiredDentalTeam";
        public const string RequiredPatientMessageName = "RequiredPatient";
        public const string RequiredTreatmentMessageName = "RequiredTreatment";
        public const string RequiredStartMessageName = "RequiredStart";
        public const string RequiredEndMessageName = "RequiredEnd";
        public const string InvalidPeriodMessageName = "InvalidPeriod";
        public const string MaxDurationMessageName = "MaxDuration";
        public const string UnsupportedStatusMessageName = "UnsupportedStatus";

        public TreatmentSessionValidator(IStringLocalizer<TreatmentSessionValidator> localizer)
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

            RuleFor(model => model.Start)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer[RequiredStartMessageName]);

            RuleFor(model => model.End)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer[RequiredEndMessageName])
                .GreaterThan(model => model.Start)
                .WithMessage(localizer[InvalidPeriodMessageName])
                .LessThanOrEqualTo(model => model.Start.HasValue ? model.Start.Value.AddHours(2) : model.End.Value)
                .WithMessage(localizer[MaxDurationMessageName]);

            RuleFor(model => model.Status)
                .IsEnumName(typeof(TreatmentSessionStatus))
                .WithMessage(localizer[UnsupportedStatusMessageName]);
        }
    }
}