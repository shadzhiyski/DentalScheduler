using System;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Identity.Dto.Input;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Output;
using DentalSystem.Presentation.Web.Api.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading;
using MediatR;
using DentalSystem.Application.UseCases.Identity.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace DentalSystem.Presentation.Web.Api.Identity
{
    /// <summary>
    /// User.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : BaseApiController
    {
        /// <summary>
        /// Creates User Controller.
        /// </summary>
        /// <param name="mediator"></param>
        public UserController(Lazy<IMediator> mediator)
        {
            Mediator = mediator;
        }

        private Lazy<IMediator> Mediator { get; }

        /// <summary>
        /// Gets logged in user avatar.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns user avatar</response>
        [HttpGet]
        [Route("avatar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Tags = new string[] { "Identity" })]
        public async Task<IActionResult> GetAvatar()
        {
            var result = await Mediator.Value.Send(new GetUserProfileInput());

            return File(result.Avatar, "image/jpeg");
        }

        /// <summary>
        /// Gets logged in user's profile.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns user profile</response>
        [HttpGet]
        [Route("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Tags = new string[] { "Identity" })]
        public async Task<IUserProfileOutput> GetProfile()
        {
            var result = await Mediator.Value.Send(new GetUserProfileInput());

            return result;
        }

        /// <summary>
        /// Updates logged in user profile.
        /// </summary>
        /// <param name="input">User profile input</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">User profile successfully updated</response>
        /// <response code="400">Returns errors</response>
        [HttpPost]
        [Route("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Tags = new string[] { "Identity" })]
        public async Task<IActionResult> UpdateProfile([FromForm] UserProfileInput input, CancellationToken cancellationToken)
        {
            var result = await Mediator.Value.Send(input, cancellationToken);

            return PresentResult(result);
        }
    }
}