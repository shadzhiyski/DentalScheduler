using System;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.UseCases.Scheduling.Dto.Input;
using DentalSystem.UseCases.Scheduling.Dto.Output;
using DentalSystem.Entities;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;
using DentalSystem.Interfaces.UseCases.Scheduling.Commands;
using Mapster;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DentalSystem.Web.Api.Controllers
{
    [ApiController]
    [Route("odata/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TreatmentSessionController : BaseApiController
    {
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

        public Lazy<TypeAdapterConfig> MappingConfig { get; }

        public Lazy<IGenericRepository<TreatmentSession>> Repository { get; }

        public Lazy<IAddTreatmentSessionCommand> AddTreatmentSessionCommand { get; }

        public Lazy<IUpdateTreatmentSessionCommand> UpdateTreatmentSessionCommand { get; }

        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<TreatmentSessionOutput> Get()
        {
            return Repository.Value.AsNoTracking()
                .ProjectToType<TreatmentSessionOutput>(MappingConfig.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(TreatmentSessionInput input)
        {
            var result = await AddTreatmentSessionCommand.Value.ExecuteAsync(input);

            return PresentResult(result);
        }

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