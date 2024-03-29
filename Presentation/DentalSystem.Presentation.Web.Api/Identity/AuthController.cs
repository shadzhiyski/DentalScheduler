using System;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Identity.Dto.Input;
using DentalSystem.Presentation.Web.Api.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace DentalSystem.Presentation.Web.Api.Identity
{
    /// <summary>
    /// Authentication.
    /// </summary>
    [ApiController]
    [Route ("api/[controller]")]
    [Authorize (AuthenticationSchemes = "Bearer")]
    public class AuthController : BaseApiController
    {
        private Lazy<IMediator> Mediator { get; }

        /// <summary>
        /// Creates Auth Controller.
        /// </summary>
        /// <param name="mediator"></param>
        public AuthController(Lazy<IMediator> mediator)
        {
            Mediator = mediator;
        }

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="model">User registration input</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">User successfully registered</response>
        /// <response code="400">Returns errors</response>
        [HttpPost]
        [AllowAnonymous]
        [Route ("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Tags = new string[] { "Identity" })]
        public async Task<IActionResult> Register(RegisterUserInput model, CancellationToken cancellationToken)
        {
            var result =  await Mediator.Value.Send(model, cancellationToken);
            return PresentResult(result);
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="loginCredentials">User credentials input</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">User successfully logged in</response>
        /// <response code="400">Returns errors</response>
        [HttpPost]
        [AllowAnonymous]
        [Route ("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Tags = new string[] { "Identity" })]
        public async Task<IActionResult> Login([FromBody] UserCredentialsInput loginCredentials, CancellationToken cancellationToken)
        {
            var result =  await Mediator.Value.Send(loginCredentials, cancellationToken);
            return PresentResult(result);
        }
    }
}