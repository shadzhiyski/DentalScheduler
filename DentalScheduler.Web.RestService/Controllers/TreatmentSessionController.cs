using System.Linq;
using System.Threading.Tasks;
using DentalScheduler.DTO.Input;
using DentalScheduler.DTO.Output;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Gateways;
using DentalScheduler.Interfaces.UseCases;
using Mapster;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.RestService.Controllers
{
    [ApiController]
    [Route("odata/[controller]")]
    // [Authorize(AuthenticationSchemes = "Bearer")]
    public class TreatmentSessionController : BaseApiController
    {
        public TreatmentSessionController(
            TypeAdapterConfig mappingConfig,
            IGenericRepository<TreatmentSession> repository,
            IAddTreatmentSessionCommand addTreatmentSessionCommand,
            IUpdateTreatmentSessionCommand updateTreatmentSessionCommand)
        {
            MappingConfig = mappingConfig;
            Repository = repository;
            AddTreatmentSessionCommand = addTreatmentSessionCommand;
            UpdateTreatmentSessionCommand = updateTreatmentSessionCommand;
        }

        public TypeAdapterConfig MappingConfig { get; }

        public IGenericRepository<TreatmentSession> Repository { get; }

        public IAddTreatmentSessionCommand AddTreatmentSessionCommand { get; }

        public IUpdateTreatmentSessionCommand UpdateTreatmentSessionCommand { get; }

        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public async Task<IQueryable<TreatmentSessionOutput>> GetAsync(ODataQueryOptions<TreatmentSession> options)
        {
            return options.ApplyTo(Repository.AsQueryable())
                .ProjectToType<TreatmentSessionOutput>(MappingConfig);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(TreatmentSessionInput input)
        {
            var result = await AddTreatmentSessionCommand.ExecuteAsync(input);

            return PresentResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(TreatmentSessionInput input)
        {
            var result = await UpdateTreatmentSessionCommand.ExecuteAsync(input);

            return PresentResult(result);
        }
    }
}