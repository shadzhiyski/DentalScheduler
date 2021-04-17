using System;
using System.Threading.Tasks;
using DentalSystem.UseCases.Identity.Dto.Input;
using DentalSystem.Boundaries.UseCases.Identity.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DentalSystem.Web.Api.Controllers
{
    /// <summary>
    /// Authentication.
    /// </summary>
    [ApiController]
    [Route ("api/[controller]")]
    [Authorize (AuthenticationSchemes = "Bearer")]
    public class AuthController : BaseApiController
    {
        private Lazy<ILoginCommand> LoginCommand { get; }

        private Lazy<IRegisterUserCommand> RegisterUserCommand { get; }

        /// <summary>
        /// Creates Auth Controller.
        /// </summary>
        /// <param name="loginCommand"></param>
        /// <param name="registerUserCommand"></param>
        public AuthController(
            Lazy<ILoginCommand> loginCommand,
            Lazy<IRegisterUserCommand> registerUserCommand)
        {
            RegisterUserCommand = registerUserCommand;
            LoginCommand = loginCommand;
        }

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="model">User registration input</param>
        /// <returns></returns>
        /// <response code="200">User successfully registered</response>
        /// <response code="400">Returns errors</response>
        [HttpPost]
        [AllowAnonymous]
        [Route ("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterUserInput model)
        {
            var result =  await RegisterUserCommand.Value.RegisterAsync(model);
            return PresentResult(result);
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="loginCredentials">User credentials input</param>
        /// <returns></returns>
        /// <response code="200">User successfully logged in</response>
        /// <response code="400">Returns errors</response>
        [HttpPost]
        [AllowAnonymous]
        [Route ("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] UserCredentialsInput loginCredentials)
        {
            var result =  await LoginCommand.Value.LoginAsync(loginCredentials);
            return PresentResult(result);
        }
    }
}