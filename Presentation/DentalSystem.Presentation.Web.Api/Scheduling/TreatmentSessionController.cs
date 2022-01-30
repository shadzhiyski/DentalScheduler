using System;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Scheduling.Dto.Input;
using DentalSystem.Application.UseCases.Scheduling.Dto.Output;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Presentation.Web.Api.Common;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Query;
using System.Threading;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace DentalSystem.Presentation.Web.Api.Scheduling
{
    /// <summary>
    /// Treatment Session.
    /// </summary>
    [ApiController]
    [Route("odata/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TreatmentSessionController : BaseApiController
    {
        /// <summary>
        /// Creates Treatment Session Controller.
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mappingConfig"></param>
        /// <param name="repository"></param>
        public TreatmentSessionController(
            Lazy<IMediator> mediator,
            Lazy<TypeAdapterConfig> mappingConfig,
            Lazy<IReadRepository<TreatmentSession>> repository)
        {
            Mediator = mediator;
            MappingConfig = mappingConfig;
            Repository = repository;
        }

        private Lazy<IMediator> Mediator { get; }

        private Lazy<TypeAdapterConfig> MappingConfig { get; }

        private Lazy<IReadRepository<TreatmentSession>> Repository { get; }

        /// <summary>
        /// Gets Treatment Sessions.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns treatment sessions</response>
        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        [SwaggerOperation(Tags = new string[] { "Scheduling" })]
        public IQueryable<TreatmentSessionOutput> Get()
        {
            return Repository.Value.AsNoTracking()
                .ProjectToType<TreatmentSessionOutput>(MappingConfig.Value);
        }

        /// <summary>
        /// Creates treatment session.
        /// </summary>
        /// <param name="input">Treatment session input</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="201">Returns message for successfully created treatment session</response>
        /// <response code="400">Returns errors</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Tags = new string[] { "Scheduling" })]
        public async Task<IActionResult> PostAsync(TreatmentSessionInput input, CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Value.Send(input, cancellationToken);

            return PresentResult(result);
        }

        /// <summary>
        /// Updates treatment session.
        /// </summary>
        /// <param name="input">Treatment session input</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="201">Returns message for successfully updated treatment session</response>
        /// <response code="404">Treatment session not found</response>
        /// <response code="400">Returns errors</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Tags = new string[] { "Scheduling" })]
        public async Task<IActionResult> PutAsync(UpdateTreatmentSessionInput input, CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Value.Send(input, cancellationToken);

            return PresentResult(result);
        }
    }
}