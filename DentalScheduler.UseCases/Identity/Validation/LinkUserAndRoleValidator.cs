using DentalScheduler.Interfaces.Dto.Input;
using FluentValidation;

namespace DentalScheduler.UseCases.Identity.Validation
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