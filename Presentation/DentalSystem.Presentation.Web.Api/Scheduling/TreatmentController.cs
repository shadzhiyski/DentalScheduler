using System.Collections.Generic;
using System.Linq;
using DentalSystem.Application.UseCases.Scheduling.Dto.Output;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Presentation.Web.Api.Common;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DentalSystem.Presentation.Web.Api.Scheduling
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
            IReadRepository<Treatment> repository)
        {
            MappingConfig = mappingConfig;
            Repository = repository;
        }

        private TypeAdapterConfig MappingConfig { get; }

        private IReadRepository<Treatment> Repository { get; }

        /// <summary>
        /// Gets Treatments.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns treatments</response>
        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        [SwaggerOperation(Tags = new string[] { "Scheduling" })]
        public IQueryable<TreatmentOutput> Get()
        {
            return Repository.AsNoTracking()
                .ProjectToType<TreatmentOutput>(MappingConfig);
        }
    }
}