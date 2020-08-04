using System.Threading.Tasks;
using DentalScheduler.Interfaces.Infrastructure;
using DentalScheduler.Interfaces.Models.Output;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.Web.RestService.Helpers
{
    public class RoleService : IRoleService<IdentityRole>
    {
        public RoleManager<IdentityRole> RoleManager { get; }

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }
        public async Task<IAuthResult> CreateAsync(IdentityRole role)
        {
            return (await RoleManager.CreateAsync(role)).Adapt<IAuthResult>();
        }

        public async Task<IdentityRole> FindByNameAsync(string name)
        {
            return await RoleManager.FindByNameAsync(name);
        }
    }
}