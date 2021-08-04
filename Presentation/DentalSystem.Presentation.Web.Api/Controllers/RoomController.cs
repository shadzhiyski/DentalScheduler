using System.Linq;
using System.Threading.Tasks;
using DentalSystem.Application.UseCases.Scheduling.Dto.Output;
using DentalSystem.Domain.Scheduling;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace DentalSystem.Presentation.Web.Api.Controllers
{
    /// <summary>
    /// Room.
    /// </summary>
    [ApiController]
    [Route("odata/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class RoomController : BaseApiController
    {
        /// <summary>
        /// Creates Room Controller.
        /// </summary>
        /// <param name="mappingConfig"></param>
        /// <param name="repository"></param>
        public RoomController(
            TypeAdapterConfig mappingConfig,
            IReadRepository<Room> repository)
        {
            MappingConfig = mappingConfig;
            Repository = repository;
        }

        private TypeAdapterConfig MappingConfig { get; }

        private IReadRepository<Room> Repository { get; }

        /// <summary>
        /// Gets Rooms.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns rooms</response>
        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<RoomOutput> Get()
        {
            return Repository.AsNoTracking()
                .ProjectToType<RoomOutput>(MappingConfig);
        }
    }
}