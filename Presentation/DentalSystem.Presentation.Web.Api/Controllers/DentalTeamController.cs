using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Scheduling.Dto.Output;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using DentalSystem.Presentation.Web.Api.Common;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DentalSystem.Presentation.Web.Api.Controllers
{
    /// <summary>
    /// Dental Team.
    /// </summary>
    [ApiController]
    [Route("odata/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class DentalTeamController : BaseApiController
    {
        /// <summary>
        /// Creates Dental Team Controller.
        /// </summary>
        /// <param name="mappingConfig"></param>
        /// <param name="repository"></param>
        public DentalTeamController(
            TypeAdapterConfig mappingConfig,
            IReadRepository<DentalTeam> repository)
        {
            MappingConfig = mappingConfig;
            Repository = repository;
        }

        private TypeAdapterConfig MappingConfig { get; }

        private IReadRepository<DentalTeam> Repository { get; }

        /// <summary>
        /// Gets Dental Teams.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns dental teams</response>
        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        [SwaggerOperation(Tags = new string[] { "Scheduling" })]
        public IQueryable<DentalTeamOutput> Get()
        {
            return Repository.AsNoTracking()
                .ProjectToType<DentalTeamOutput>(MappingConfig);
        }
    }
}