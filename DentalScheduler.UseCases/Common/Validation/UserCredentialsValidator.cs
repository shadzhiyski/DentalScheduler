using System;
using System.Threading;
using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Input;
using FluentValidation;

namespace DentalScheduler.UseCases.Common.Validation
{
    public class UserCredentialsValidator : AbstractValidator<IUserCredentialsInput>
    {
        public UserCredentialsValidator()
        {
            RuleFor(userInput => userInput.UserName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();

            RuleFor(userInput => userInput.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();
            
            RuleFor(userInput => userInput.Password)
                .MinimumLength(6);
        }
    }
}