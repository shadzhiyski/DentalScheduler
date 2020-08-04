using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DentalScheduler.DTO.Input;
using DentalScheduler.Interfaces.Models.Output.Common;
using DentalScheduler.Interfaces.UseCases;
using DentalScheduler.Web.RestService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DentalScheduler.Web.RestService.Controllers 
{
    [ApiController]
    [Route ("api/[controller]")]
    [Authorize (AuthenticationSchemes = "Bearer")]
    public class AuthController : BaseApiController 
    {
        public ILoginCommand LoginCommand { get; }

        public IRegisterUserCommand RegisterUserCommand { get; }

        public AuthController(
            ILoginCommand loginCommand, 
            IRegisterUserCommand registerUserCommand) 
        {
            RegisterUserCommand = registerUserCommand;
            LoginCommand = loginCommand;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route ("register")]
        public async Task<IActionResult> Register(UserCredentialsInput model) 
        {
            var result =  await RegisterUserCommand.RegisterAsync(model);
            return PresentResult(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route ("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsInput loginCredentials) 
        {
            var result =  await LoginCommand.LoginAsync(loginCredentials);
            return PresentResult(result);
        }
    }
}