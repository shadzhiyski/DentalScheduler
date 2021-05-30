using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.Infrastructure.Identity.Dto.Output;

namespace DentalSystem.Application.Boundaries.Infrastructure.Identity
{
    public interface IUserService<TUser>
    {
        TUser CurrentUser { get; }

        Task<TUser> FindByNameAsync(string name, CancellationToken cancellationToken);

        Task<bool> CheckPasswordAsync(TUser user, string password, CancellationToken cancellationToken);

        Task<IAuthResult> CreateAsync(TUser user, string password, CancellationToken cancellationToken);

        Task<IAuthResult> AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken);

        Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken);

        Task<IAuthResult> UpdateAsync(TUser user, CancellationToken cancellationToken);
    }
}