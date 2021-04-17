using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.Infrastructure.Identity.Dto.Output;

namespace DentalSystem.Application.Boundaries.Infrastructure.Identity
{
    public interface IRoleService<TRole>
    {
        Task<TRole> FindByNameAsync(string name);

        Task<IAuthResult> CreateAsync(TRole role);
    }
}