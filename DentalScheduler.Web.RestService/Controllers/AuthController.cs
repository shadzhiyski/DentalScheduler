using System.Threading.Tasks;
using DentalScheduler.DTO.Input;
using DentalScheduler.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Register(RegisterUserInput model) 
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