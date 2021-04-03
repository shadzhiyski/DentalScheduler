using System.Collections.Generic;
using System.Linq;
using DentalSystem.UseCases.Scheduling.Dto.Output;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;
using Mapster;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;

namespace DentalSystem.Web.Api.Controllers
{
    [ApiController]
    [Route("odata/[controller]")]
    public class TreatmentController : BaseApiController
    {
        public TreatmentController(
            TypeAdapterConfig mappingConfig,
            IGenericRepository<Treatment> repository)
        {
            MappingConfig = mappingConfig;
            Repository = repository;
        }

        public TypeAdapterConfig MappingConfig { get; }

        public IGenericRepository<Treatment> Repository { get; }

        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<TreatmentOutput> Get()
        {
            return Repository.AsNoTracking()
                .ProjectToType<TreatmentOutput>(MappingConfig);
        }
    }
}