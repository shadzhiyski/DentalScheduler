using System;
using System.Linq;
using System.Threading.Tasks;
using DentalScheduler.UseCases.Scheduling.Dto.Input;
using DentalScheduler.UseCases.Scheduling.Dto.Output;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using DentalScheduler.Interfaces.UseCases.Scheduling;
using DentalScheduler.Web.Api.Models;
using Mapster;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.Api.Controllers
{
    [ApiController]
    [Route("odata/[controller]")]
    // [Authorize(AuthenticationSchemes = "Bearer")]
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
        public IQueryable<TreatmentSessionOutput> Get([FromQuery] ODataParametersInputModel filter)
        {
            return Repository.Value.AsQueryable()
                .ProjectToType<TreatmentSessionOutput>(MappingConfig.Value);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(TreatmentSessionInput input)
        {
            var result = await AddTreatmentSessionCommand.Value.ExecuteAsync(input);

            return PresentResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(TreatmentSessionInput input)
        {
            var result = await UpdateTreatmentSessionCommand.Value.ExecuteAsync(input);

            return PresentResult(result);
        }
    }
}