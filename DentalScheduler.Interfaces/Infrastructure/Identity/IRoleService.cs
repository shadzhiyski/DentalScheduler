using System.Threading.Tasks;
using DentalScheduler.Interfaces.Infrastructure.Identity.Dto.Output;

namespace DentalScheduler.Interfaces.Infrastructure.Identity
{
    public interface IRoleService<TRole>
    {
        Task<TRole> FindByNameAsync(string name);

        Task<IAuthResult> CreateAsync(TRole role);
    }
}