using System.Threading.Tasks;
using DentalSystem.Boundaries.Infrastructure.Identity.Dto.Output;

namespace DentalSystem.Boundaries.Infrastructure.Identity
{
    public interface IRoleService<TRole>
    {
        Task<TRole> FindByNameAsync(string name);

        Task<IAuthResult> CreateAsync(TRole role);
    }
}