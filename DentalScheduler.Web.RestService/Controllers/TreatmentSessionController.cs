using System.Linq;
using System.Threading.Tasks;
using DentalScheduler.DTO.Output;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Gateways;
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
            IGenericRepository<TreatmentSession> repository)
        {
            MappingConfig = mappingConfig;
            Repository = repository;
        }

        public TypeAdapterConfig MappingConfig { get; }

        public IGenericRepository<TreatmentSession> Repository { get; }

        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public async Task<IQueryable<TreatmentSessionOutput>> GetAsync(ODataQueryOptions<TreatmentSession> options)
        {
            return options.ApplyTo(Repository.AsQueryable())
                .ProjectToType<TreatmentSessionOutput>(MappingConfig);
        }
    }
}