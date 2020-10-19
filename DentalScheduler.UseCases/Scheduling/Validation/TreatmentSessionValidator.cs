using System;
using System.Threading;
using System.Threading.Tasks;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Input;
using FluentValidation;

namespace DentalScheduler.UseCases.Scheduling.Validation
{
    public class TreatmentSessionValidator : AbstractValidator<ITreatmentSessionInput>
    {
        public TreatmentSessionValidator()
        {
            RuleFor(model => model.DentalTeamReferenceId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.PatientReferenceId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.TreatmentReferenceId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.Start)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.End)
                .Cascade(CascadeMode.StopOnFirstFailure)
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