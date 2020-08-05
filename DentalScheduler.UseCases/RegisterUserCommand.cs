using System;
using System.Threading.Tasks;
using DentalScheduler.DTO.Input;
using DentalScheduler.DTO.Output;
using DentalScheduler.DTO.Output.Common;
using DentalScheduler.Interfaces.Infrastructure;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases;
using DentalScheduler.Interfaces.UseCases.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DentalScheduler.UseCases
{
    public class RegisterUserCommand : IRegisterUserCommand
    {
        public IConfiguration Config { get; }

        public IUserService<IdentityUser> UserService { get; }

        public IApplicationValidator<IUserCredentialsInput> Validator { get; }

        public IJwtAuthManager JwtAuthManager { get; }

        public ILoginCommand LoginCommand { get; }

        public RegisterUserCommand(
            IConfiguration config,
            IUserService<IdentityUser> userService, 
            IApplicationValidator<IUserCredentialsInput> validator,
            IJwtAuthManager jwtAuthManager,
            ILoginCommand loginCommand)
        {
            Config = config;
            UserService = userService;
            Validator = validator;
            JwtAuthManager = jwtAuthManager;
            LoginCommand = loginCommand;
        }

        public async Task<IResult<IAccessTokenOutput>> RegisterAsync(IUserCredentialsInput userInput)
        {
            var validationResult = Validator.Validate(userInput);
            if (validationResult.Errors.Count > 0)
            {
                return new Result<IAccessTokenOutput>(validationResult.Errors);
            }

            var user = new IdentityUser
            {
                Email = userInput.UserName,
                UserName = userInput.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var identityResult = await UserService.CreateAsync(user, userInput.Password);

            if (!identityResult.Succeeded)
            {
                return new Result<IAccessTokenOutput>(identityResult.Errors);
            }

            // Auto login after registrаtion (successful user registration should return access_token)
            return await LoginCommand.LoginAsync(new UserCredentialsInput()
            {
                UserName = userInput.UserName,
                Password = userInput.Password
            });
        }
    }
}