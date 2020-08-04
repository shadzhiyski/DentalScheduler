using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.Interfaces.Infrastructure
{
    public interface IRoleService<TRole>
    {
        Task<TRole> FindByNameAsync(string name);

        Task<IAuthResult> CreateAsync(TRole role);
    }
}