using DentalSystem.Interfaces.UseCases.Identity.Dto.Input;
using FluentValidation;

namespace DentalSystem.UseCases.Identity.Validation
{
    public class CreateRoleValidator : AbstractValidator<ICreateRoleInput>
    {
        public CreateRoleValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}