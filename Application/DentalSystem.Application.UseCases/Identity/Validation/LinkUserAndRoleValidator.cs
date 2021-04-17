using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using FluentValidation;

namespace DentalSystem.Application.UseCases.Identity.Validation
{
    public class LinkUserAndRoleValidator : AbstractValidator<ILinkUserAndRoleInput>
    {
        public LinkUserAndRoleValidator()
        {
            RuleFor(model => model.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull();

            RuleFor(model => model.RoleName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull();
        }
    }
}