using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.Interfaces.Infrastructure;
using DentalScheduler.Interfaces.Models.Output;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.Web.RestService.Helpers
{
    public class UserService : IUserService<IdentityUser>
    {
        public TypeAdapterConfig MappingConfig { get; }
        public UserManager<IdentityUser> UserManager { get; }

        public UserService(TypeAdapterConfig mappingConfig, UserManager<IdentityUser> userManager)
        {
            MappingConfig = mappingConfig;
            UserManager = userManager;

        }
        public async Task<IAuthResult> AddToRoleAsync(IdentityUser user, string roleName)
        {
            return (await UserManager.AddToRoleAsync(user, roleName))
                .Adapt<IAuthResult>(MappingConfig);
        }

        public async Task<bool> CheckPasswordAsync(IdentityUser user, string password)
        {
            return (await UserManager.CheckPasswordAsync(user, password));
        }

        public async Task<IAuthResult> CreateAsync(IdentityUser user, string password)
        {
            return (await UserManager.CreateAsync(user, password))
                .Adapt<IAuthResult>(MappingConfig);
        }

        public async Task<IdentityUser> FindByNameAsync(string name)
        {
            return await UserManager.FindByNameAsync(name);
        }

        public async Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            return await UserManager.GetRolesAsync(user);
        }
    }
}