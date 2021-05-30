using System.Threading;
using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.Infrastructure.Identity;
using DentalSystem.Application.Boundaries.Infrastructure.Identity.Dto.Output;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace DentalSystem.Infrastructure.Identity.Services
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
        public async Task<IAuthResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return (await RoleManager.CreateAsync(role))
                .Adapt<IAuthResult>(MappingConfig);
        }

        public async Task<IdentityRole> FindByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await RoleManager.FindByNameAsync(name);
        }
    }
}