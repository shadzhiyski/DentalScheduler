using System;
using System.Threading.Tasks;
using DentalScheduler.DTO.Input;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Interfaces.UseCases.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.RestService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : BaseApiController 
    {
        public UserController(
            Lazy<IGetUserProfileQuery> getUserProfileQuery) 
        {
            GetUserProfileQuery = getUserProfileQuery;
        }

        public Lazy<IGetUserProfileQuery> GetUserProfileQuery { get; }

        [HttpGet]
        [Route ("profile")]
        public async Task<IUserProfileOutput> GetProfile() 
        {
            var currentUserName = User.Identity.Name;
            var result = await GetUserProfileQuery.Value.ExecuteAsync(currentUserName);
            
            return result;
        }
    }
}