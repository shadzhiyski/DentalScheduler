using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DentalScheduler.Web.RestService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.RestService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles=Roles.Admin)]
    public class RolesController : ControllerBase
    {
        public RoleManager<IdentityRole> RoleManager { get; }

        public UserManager<IdentityUser> UserManager { get; }

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        [HttpPost]
        [Route("/create")]
        public async Task<IActionResult> Create([FromBody] RoleInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ValidationState);
            }

            var role = new IdentityRole()
            {
                Name = model.Name
            };

            await RoleManager.CreateAsync(role);

            return Ok($"Role \"{model.Name}\" created.");
        }

        [HttpPost]
        [Route("/user/update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserRoleInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ValidationState);
            }

            var role = await RoleManager.FindByNameAsync(model.RoleName);
            if (role == null)
            {
                ModelState.AddModelError(nameof(model.RoleName), "Role doesn't exist");
                return BadRequest(ModelState);
            }

            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError(nameof(model.RoleName), "User doesn't exist");
                return BadRequest(ModelState);
            }
            
            await UserManager.AddToRoleAsync(user, role.Name);

            return Ok($"\"{model.RoleName}\" role set to user \"{model.UserName}\"");
        }
    }
}