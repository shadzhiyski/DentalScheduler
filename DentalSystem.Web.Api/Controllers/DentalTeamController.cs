using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.UseCases.Scheduling.Dto.Output;
using DentalSystem.Entities;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;
using Mapster;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalSystem.Web.Api.Controllers
{
    [ApiController]
    [Route("odata/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
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
        public IQueryable<DentalTeamOutput> Get()
        {
            return Repository.AsNoTracking()
                .ProjectToType<DentalTeamOutput>(MappingConfig);
        }
    }
}