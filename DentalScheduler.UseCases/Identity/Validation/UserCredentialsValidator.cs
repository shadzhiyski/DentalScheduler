using System;
using System.Threading;
using System.Threading.Tasks;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DentalScheduler.UseCases.Identity.Validation
{
    public class UserCredentialsValidator : AbstractValidator<IUserCredentialsInput>
    {
        public UserCredentialsValidator(IStringLocalizer<UserCredentialsValidator> localizer)
        {
            RuleFor(userInput => userInput.UserName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithMessage(localizer["MissingUserName"]);

            RuleFor(userInput => userInput.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithMessage(localizer["MissingPassword"]);

            RuleFor(userInput => userInput.Password)
                .MinimumLength(6)
                .WithMessage(localizer["ShortPassword"]);
        }
    }
}