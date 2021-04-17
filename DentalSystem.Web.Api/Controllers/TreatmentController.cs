using System.Collections.Generic;
using System.Linq;
using DentalSystem.UseCases.Scheduling.Dto.Output;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Boundaries.Infrastructure.Common.Persistence;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace DentalSystem.Web.Api.Controllers
{
    /// <summary>
    /// Treatment.
    /// </summary>
    [ApiController]
    [Route("odata/[controller]")]
    public class TreatmentController : BaseApiController
    {
        /// <summary>
        /// Creates Treatment Controller.
        /// </summary>
        /// <param name="mappingConfig"></param>
        /// <param name="repository"></param>
        public TreatmentController(
            TypeAdapterConfig mappingConfig,
            IGenericRepository<Treatment> repository)
        {
            MappingConfig = mappingConfig;
            Repository = repository;
        }

        private TypeAdapterConfig MappingConfig { get; }

        private IGenericRepository<Treatment> Repository { get; }

        /// <summary>
        /// Gets Treatments.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns treatments</response>
        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<TreatmentOutput> Get()
        {
            return Repository.AsNoTracking()
                .ProjectToType<TreatmentOutput>(MappingConfig);
        }
    }
}