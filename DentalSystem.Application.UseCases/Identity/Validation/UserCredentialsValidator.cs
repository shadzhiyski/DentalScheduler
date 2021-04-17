using System;
using System.Threading;
using System.Threading.Tasks;
using DentalSystem.Boundaries.UseCases.Identity.Dto.Input;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DentalSystem.Application.UseCases.Identity.Validation
{
    public class UserCredentialsValidator : AbstractValidator<IUserCredentialsInput>
    {
        public UserCredentialsValidator(IStringLocalizer<UserCredentialsValidator> localizer)
        {
            RuleFor(userInput => userInput.UserName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage(localizer["MissingUserName"]);

            RuleFor(userInput => userInput.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage(localizer["MissingPassword"]);

            RuleFor(userInput => userInput.Password)
                .MinimumLength(6)
                .WithMessage(localizer["ShortPassword"]);
        }
    }
}