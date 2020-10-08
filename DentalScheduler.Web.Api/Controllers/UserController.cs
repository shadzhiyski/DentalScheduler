using System;
using System.IO;
using System.Threading.Tasks;
using DentalScheduler.UseCases.Identity.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Identity.Commands;
using DentalScheduler.Interfaces.UseCases.Identity.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : BaseApiController 
    {
        public UserController(
            Lazy<IGetUserProfileQuery> getUserProfileQuery,
            Lazy<IUpdateProfileCommand> updateProfileCommand) 
        {
            GetUserProfileQuery = getUserProfileQuery;
            UpdateProfileCommand = updateProfileCommand;
        }

        public Lazy<IGetUserProfileQuery> GetUserProfileQuery { get; }

        public Lazy<IUpdateProfileCommand> UpdateProfileCommand { get; }

        [HttpGet]
        [Route("avatar")]
        public async Task<IActionResult> GetAvatar() 
        {
            var result = await GetUserProfileQuery.Value.ExecuteAsync();

            return File(result.Avatar, "image/jpeg");
        }

        [HttpGet]
        [Route("profile")]
        public async Task<IUserProfileOutput> GetProfile() 
        {
            var result = await GetUserProfileQuery.Value.ExecuteAsync();
            
            return result;
        }

        [HttpPost]
        [Route("profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UserProfileInput input) 
        {
            var result = await UpdateProfileCommand.Value.ExecuteAsync(input);
            
            return PresentResult(result);
        }
    }
}