using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.Interfaces.Infrastructure
{
    public interface IUserService<TUser>
    {
        Task<TUser> FindByNameAsync(string name);
        
        Task<IAuthResult> CreateAsync(TUser user, string password);

        Task<IAuthResult> AddToRoleAsync(TUser user, string roleName);

        Task<IList<string>> GetRolesAsync(TUser user);
    }
}