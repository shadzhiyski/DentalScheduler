using DentalScheduler.Interfaces.Models.Input;
using FluentValidation;

namespace DentalScheduler.UseCases.Validation
{
    public class LinkUserAndRoleValidator : AbstractValidator<ILinkUserAndRoleInput>
    {
        public LinkUserAndRoleValidator()
        {
            RuleFor(model => model.UserName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull();
            
            RuleFor(model => model.RoleName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull();
        }
    }
}