using System.Linq;
using DentalSystem.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.UseCases.Common.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DentalSystem.UseCases.Identity.Validation
{
    public class UserProfileValidator : AbstractValidator<IUserProfileInput>
    {
        public const double AvatarMaxAllowedSizeInMb
            = (double)Constants.UserProfileInput.AvatarMaxAllowedSizeInBytes / (1024 * 1024);
        public UserProfileValidator(
            IStringLocalizer<UserProfileValidator> localizer,
            ImageValidator imageValidator)
        {
            RuleFor(model => model.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer["MissingFirstName"])
                .NotEmpty()
                .WithMessage(localizer["MissingFirstName"]);

            RuleFor(model => model.LastName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer["MissingLastName"])
                .NotEmpty()
                .WithMessage(localizer["MissingLastName"]);

            RuleFor(model => model.Avatar)
                .SetValidator(imageValidator);

            RuleFor(model => model.Avatar)
                .Must(avatar => avatar.LongLength <= Constants.UserProfileInput.AvatarMaxAllowedSizeInBytes)
                .WithMessage(string.Format(localizer["AvatarFileSizeTooLarge"], AvatarMaxAllowedSizeInMb));
        }
    }
}