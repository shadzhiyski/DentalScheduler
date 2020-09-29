using System.Threading.Tasks;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using DentalScheduler.Interfaces.Dto.Output;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.Infrastructure.Identity
{
    public class RoleService : IRoleService<IdentityRole>
    {
        public TypeAdapterConfig MappingConfig { get; }
        
        public RoleManager<IdentityRole> RoleManager { get; }

        public RoleService(TypeAdapterConfig mappingConfig, RoleManager<IdentityRole> roleManager)
        {
            MappingConfig = mappingConfig;
            RoleManager = roleManager;
        }
        public async Task<IAuthResult> CreateAsync(IdentityRole role)
        {
            return (await RoleManager.CreateAsync(role))
                .Adapt<IAuthResult>(MappingConfig);
        }

        public async Task<IdentityRole> FindByNameAsync(string name)
        {
            return await RoleManager.FindByNameAsync(name);
        }
    }
}