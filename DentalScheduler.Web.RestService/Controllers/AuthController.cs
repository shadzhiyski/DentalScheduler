using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DentalScheduler.Web.RestService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DentalScheduler.Web.RestService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AuthController : ControllerBase
    {
        public IConfiguration Config { get; }
        
        public UserManager<IdentityUser> UserManager { get; }

        public RoleManager<IdentityRole> RoleManager { get; }

        public AuthController(
            IConfiguration configuration, 
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            Config = configuration;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(UserInputModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Invalid user data");
            }

            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = new IdentityUser
            {
                Email = model.UserName,
                UserName = model.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var identityResult = await UserManager.CreateAsync(user, model.Password);

            if (!identityResult.Succeeded)
            {
                return GetErrorResult(identityResult);
            }

            // Auto login after registrаtion (successful user registration should return access_token)
            var loginResult = await Login(new UserInputModel()
            {
                UserName = model.UserName,
                Password = model.Password
            });

            return loginResult;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserInputModel loginCredentials)
        {
            if (loginCredentials == null)
            {
                return this.BadRequest("Invalid user data");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ValidationState);
            }

            var user = await AuthenticateUserAsync(loginCredentials);
            if (user == null)
            {
                ModelState.AddModelError(nameof(UserInputModel), $"Invalid username or password");
                return BadRequest(ModelState);
            }

            var tokenString = GenerateJWTAsync(user);
            return Ok(new
            {
                access_token = tokenString,
            });
        }

        private async Task<IdentityUser> AuthenticateUserAsync(UserInputModel loginCredentials)
        {
            var user = await UserManager.FindByNameAsync(loginCredentials.UserName);
            if (!(await UserManager.CheckPasswordAsync(user, loginCredentials.Password)))
            {
                return null;
            }

            return user;
        }

        private async Task<string> GenerateJWTAsync(IdentityUser userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            int expiryInMinutes = Convert.ToInt32(Config["Jwt:ExpiryInMinutes"]);

            var user = await UserManager.FindByNameAsync(userInfo.UserName);
            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.UserName.ToString()),
                new Claim(ClaimTypes.Role, (await UserManager.GetRolesAsync(user)).FirstOrDefault()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: Config["Jwt:Issuer"],
                audience: Config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(nameof(UserInputModel), error.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}