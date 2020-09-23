using System.Collections.Generic;
using System.Linq;
using DentalScheduler.DTO.Output;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Gateways;
using DentalScheduler.Web.RestService.Models;
using Mapster;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.RestService.Controllers
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
        public IQueryable<TreatmentOutput> Get([FromQuery] ODataParametersInputModel filter)
        {
            return Repository.AsQueryable()
                .ProjectToType<TreatmentOutput>(MappingConfig);
        }
    }
}