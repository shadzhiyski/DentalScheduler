using System;
using System.Threading.Tasks;
using DentalScheduler.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.RestService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles=Roles.Admin)]
    public class RolesController : BaseApiController
    {
        public Lazy<ICreateRoleCommand> CreateRoleCommand { get; }

        public Lazy<ILinkUserAndRoleCommand> LinkUserAndRoleCommand { get; }

        public RolesController(
            Lazy<ICreateRoleCommand> createRoleCommand, 
            Lazy<ILinkUserAndRoleCommand> linkUserAndRoleCommand)
        {
            CreateRoleCommand = createRoleCommand;
            LinkUserAndRoleCommand = linkUserAndRoleCommand;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateRoleInput model)
        {
            var result = await CreateRoleCommand.Value.CreateAsync(model);
            return PresentResult(result);
        }

        [HttpPost]
        [Route("user/update")]
        public async Task<IActionResult> UpdateUser([FromBody] LinkUserAndRoleInput model)
        {
            var result = await LinkUserAndRoleCommand.Value.ExecuteAsync(model);
            return PresentResult(result);
        }
    }
}