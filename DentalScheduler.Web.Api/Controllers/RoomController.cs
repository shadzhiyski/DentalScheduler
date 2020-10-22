using System.Linq;
using System.Threading.Tasks;
using DentalScheduler.UseCases.Scheduling.Dto.Output;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using DentalScheduler.Web.Api.Models;
using Mapster;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.Api.Controllers
{
    [ApiController]
    [Route("odata/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
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
        public IQueryable<RoomOutput> Get()
        {
            return Repository.AsQueryable()
                .ProjectToType<RoomOutput>(MappingConfig);
        }
    }
}