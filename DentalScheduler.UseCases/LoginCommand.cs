using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalScheduler.DTO.Output;
using DentalScheduler.DTO.Output.Common;
using DentalScheduler.Interfaces.Infrastructure;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases;
using DentalScheduler.Interfaces.UseCases.Validation;
using DentalScheduler.UseCases.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DentalScheduler.UseCases
{
    public class LoginCommand : ILoginCommand
    {
        public IConfiguration Config { get; }

        public IUserService<IdentityUser> UserService { get; }

        public IApplicationValidator<IUserCredentialsInput> Validator { get; }

        public IJwtAuthManager JwtAuthManager { get; }

        public LoginCommand(
            IConfiguration config,
            IUserService<IdentityUser> userService, 
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
            if (!(await UserService.CheckPasswordAsync(user, userInput.Password)))
            {
                validationResult.Errors.Add(
                    new ValidationError()
                    {
                        PropertyName = nameof(IUserCredentialsInput.Password),
                        Errors = new [] { "Invalid password." }
                    }
                );

                return new Result<IAccessTokenOutput>(validationResult.Errors);
            }

            var roleName = (await UserService.GetRolesAsync(user)).FirstOrDefault();
            var tokenString = JwtAuthManager.GenerateJwt(userInput, roleName);

            return new Result<IAccessTokenOutput>(new AccessTokenOutput(tokenString));
        }
    }
}
