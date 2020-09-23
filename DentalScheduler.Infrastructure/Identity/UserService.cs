using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.Entities.Identity;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using DentalScheduler.Interfaces.Models.Output;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.Infrastructure.Identity
{
    public class UserService : IUserService<User>
    {
        public TypeAdapterConfig MappingConfig { get; }
        public UserManager<User> UserManager { get; }

        public UserService(TypeAdapterConfig mappingConfig, UserManager<User> userManager)
        {
            MappingConfig = mappingConfig;
            UserManager = userManager;

        }
        public async Task<IAuthResult> AddToRoleAsync(User user, string roleName)
        {
            return (await UserManager.AddToRoleAsync(user, roleName))
                .Adapt<IAuthResult>(MappingConfig);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return (await UserManager.CheckPasswordAsync(user, password));
        }

        public async Task<IAuthResult> CreateAsync(User user, string password)
        {
            return (await UserManager.CreateAsync(user, password))
                .Adapt<IAuthResult>(MappingConfig);
        }

        public async Task<User> FindByNameAsync(string name)
        {
            return await UserManager.FindByNameAsync(name);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await UserManager.GetRolesAsync(user);
        }
    }
}