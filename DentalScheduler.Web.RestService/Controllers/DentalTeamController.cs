using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalScheduler.Dto.Output;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using DentalScheduler.Web.RestService.Models;
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
    public class DentalTeamController : BaseApiController
    {
        public DentalTeamController(
            TypeAdapterConfig mappingConfig,
            IGenericRepository<DentalTeam> repository)
        {
            MappingConfig = mappingConfig;
            Repository = repository;
        }

        public TypeAdapterConfig MappingConfig { get; }

        public IGenericRepository<DentalTeam> Repository { get; }

        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<DentalTeamOutput> Get([FromQuery] ODataParametersInputModel filter)
        {
            return Repository.AsQueryable()
                .ProjectToType<DentalTeamOutput>(MappingConfig);
        }
    }
}