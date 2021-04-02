using System;
using System.Threading.Tasks;
using DentalSystem.UseCases.Identity.Dto.Input;
using DentalSystem.Interfaces.UseCases.Identity.Dto.Output;
using DentalSystem.Interfaces.UseCases.Identity.Commands;
using DentalSystem.Interfaces.UseCases.Identity.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalSystem.Web.Api.Controllers
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