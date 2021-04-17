using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using DentalSystem.Application.UseCases.Common.Dto.Output;
using DentalSystem.Entities.Identity;
using DentalSystem.Application.Boundaries.Infrastructure.Identity;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Identity.Commands;
using DentalSystem.Application.Boundaries.UseCases.Common.Validation;
using DentalSystem.Application.UseCases.Common.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DentalSystem.Application.UseCases.Identity.Commands
{
    public class LoginCommand : ILoginCommand
    {
        public IConfiguration Config { get; }

        public IUserService<User> UserService { get; }

        public IApplicationValidator<IUserCredentialsInput> Validator { get; }

        public IJwtAuthManager JwtAuthManager { get; }

        public LoginCommand(
            IConfiguration config,
            IUserService<User> userService,
            IApplicationValidator<IUserCredentialsInput> validator,
            IJwtAuthManager jwtAuthManager)
        {
            Config = config;
            UserService = userService;
            Validator = validator;
            JwtAuthManager = jwtAuthManager;
        }

        public async Task<IResult<IAccessTokenOutput>> LoginAsync(IUserCredentialsInput userInput)
        {
            var validationResult = Validator.Validate(userInput);
            if (validationResult.Errors.Count > 0)
            {
                return new Result<IAccessTokenOutput>(validationResult.Errors);
            }

            var user = await UserService.FindByNameAsync(userInput.UserName);
            if (user == null)
            {
                validationResult.Errors.Add(
                    new ValidationError()
                    {
                        PropertyName = nameof(IUserCredentialsInput.UserName),
                        Errors = new [] { "User does not exist." }
                    }
                );
            }

            if (user != null && !(await UserService.CheckPasswordAsync(user, userInput.Password)))
            {
                validationResult.Errors.Add(
                    new ValidationError()
                    {
                        PropertyName = nameof(IUserCredentialsInput.Password),
                        Errors = new [] { "Invalid password." }
                    }
                );
            }

            if (validationResult.Errors.Count > 0)
            {
                return new Result<IAccessTokenOutput>(validationResult.Errors);
            }

            var roleName = (await UserService.GetRolesAsync(user)).FirstOrDefault();
            var tokenString = await JwtAuthManager.GenerateJwtAsync(userInput, roleName);

            return new Result<IAccessTokenOutput>(new AccessTokenOutput(tokenString));
        }
    }
}
