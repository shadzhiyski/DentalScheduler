using System;
using System.Threading;
using System.Threading.Tasks;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Interfaces.UseCases.Scheduling.Dto.Input;
using FluentValidation;

namespace DentalSystem.UseCases.Scheduling.Validation
{
    public class TreatmentSessionValidator : AbstractValidator<ITreatmentSessionInput>
    {
        public TreatmentSessionValidator()
        {
            RuleFor(model => model.DentalTeamReferenceId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.PatientReferenceId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.TreatmentReferenceId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.Start)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.End)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThan(model => model.Start)
                .LessThanOrEqualTo(model => model.Start.Value.AddHours(2))
                .WithMessage("The duration of the treatment session must not be longer than 2 hours.");

            RuleFor(model => model.Status)
                .IsEnumName(typeof(TreatmentSessionStatus));
        }
    }
}