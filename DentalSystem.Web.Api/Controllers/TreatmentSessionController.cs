using System;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.UseCases.Scheduling.Dto.Input;
using DentalSystem.UseCases.Scheduling.Dto.Output;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Boundaries.UseCases.Scheduling.Commands;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Query;

namespace DentalSystem.Web.Api.Controllers
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
        /// <param name="mappingConfig"></param>
        /// <param name="repository"></param>
        /// <param name="addTreatmentSessionCommand"></param>
        /// <param name="updateTreatmentSessionCommand"></param>
        public TreatmentSessionController(
            Lazy<TypeAdapterConfig> mappingConfig,
            Lazy<IGenericRepository<TreatmentSession>> repository,
            Lazy<IAddTreatmentSessionCommand> addTreatmentSessionCommand,
            Lazy<IUpdateTreatmentSessionCommand> updateTreatmentSessionCommand)
        {
            MappingConfig = mappingConfig;
            Repository = repository;
            AddTreatmentSessionCommand = addTreatmentSessionCommand;
            UpdateTreatmentSessionCommand = updateTreatmentSessionCommand;
        }

        private Lazy<TypeAdapterConfig> MappingConfig { get; }

        private Lazy<IGenericRepository<TreatmentSession>> Repository { get; }

        private Lazy<IAddTreatmentSessionCommand> AddTreatmentSessionCommand { get; }

        private Lazy<IUpdateTreatmentSessionCommand> UpdateTreatmentSessionCommand { get; }

        /// <summary>
        /// Gets Treatment Sessions.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns treatment sessions</response>
        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<TreatmentSessionOutput> Get()
        {
            return Repository.Value.AsNoTracking()
                .ProjectToType<TreatmentSessionOutput>(MappingConfig.Value);
        }

        /// <summary>
        /// Creates treatment session.
        /// </summary>
        /// <param name="input">Treatment session input</param>
        /// <returns></returns>
        /// <response code="201">Returns message for successfully created treatment session</response>
        /// <response code="400">Returns errors</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(TreatmentSessionInput input)
        {
            var result = await AddTreatmentSessionCommand.Value.ExecuteAsync(input);

            return PresentResult(result);
        }

        /// <summary>
        /// Updates treatment session.
        /// </summary>
        /// <param name="input">Treatment session input</param>
        /// <returns></returns>
        /// <response code="201">Returns message for successfully updated treatment session</response>
        /// <response code="404">Treatment session not found</response>
        /// <response code="400">Returns errors</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync(TreatmentSessionInput input)
        {
            var result = await UpdateTreatmentSessionCommand.Value.ExecuteAsync(input);

            return PresentResult(result);
        }
    }
}