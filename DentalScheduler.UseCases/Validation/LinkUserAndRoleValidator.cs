using DentalScheduler.Interfaces.Models.Input;
using FluentValidation;

namespace DentalScheduler.UseCases.Validation
{
    public class LinkUserAndRoleValidator : AbstractValidator<ILinkUserAndRoleInput>
    {
        public LinkUserAndRoleValidator()
        {
            RuleFor(model => model.UserName)
                .NotEmpty()
                .NotNull();
            
            RuleFor(model => model.RoleName)
                .NotEmpty()
                .NotNull();
        }
    }
}