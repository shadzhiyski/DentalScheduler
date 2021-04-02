using System.Threading.Tasks;
using DentalSystem.Interfaces.Infrastructure.Identity.Dto.Output;

namespace DentalSystem.Interfaces.Infrastructure.Identity
{
    public interface IRoleService<TRole>
    {
        Task<TRole> FindByNameAsync(string name);

        Task<IAuthResult> CreateAsync(TRole role);
    }
}