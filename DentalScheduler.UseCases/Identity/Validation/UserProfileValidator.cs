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
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.LastName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.Avatar)
                .SetValidator(imageValidator);

            RuleFor(model => model.Avatar)
                .Must(avatar => avatar.LongLength <= Constants.UserProfileInput.AvatarMaxAllowedSizeInBytes)
                .WithMessage($"File size too large. Maximum allowed size: {(double)Constants.UserProfileInput.AvatarMaxAllowedSizeInBytes / (1024 * 1024):0.0} MB.");
        }
    }
}