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
    [Route("api/[controller]")]
    // [Authorize(AuthenticationSchemes = "Bearer")]
    public class RoomController : BaseApiController
    {
        public RoomController(
            TypeAdapterConfig mappingConfig,
            IGenericRepository<Room> repository)
        {
            MappingConfig = mappingConfig;
            Repository = repository;
        }

        public TypeAdapterConfig MappingConfig { get; }

        public IGenericRepository<Room> Repository { get; }

        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public async Task<IQueryable<RoomOutput>> GetAsync(ODataQueryOptions<Room> options)
        {
            return options.ApplyTo(Repository.AsQueryable())
                .ProjectToType<RoomOutput>(MappingConfig);
        }
    }
}