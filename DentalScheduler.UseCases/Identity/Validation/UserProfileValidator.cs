using System.Linq;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using DentalScheduler.UseCases.Common.Validation;
using FluentValidation;

namespace DentalScheduler.UseCases.Identity.Validation
{
    public class UserProfileValidator : AbstractValidator<IUserProfileInput>
    {
        public UserProfileValidator(ImageValidator imageValidator)
        {
            RuleFor(model => model.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();
            
            RuleFor(model => model.Avatar)
                .SetValidator(imageValidator);
        }
    }
}