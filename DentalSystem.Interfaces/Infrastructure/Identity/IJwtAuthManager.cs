using System.Threading.Tasks;
using DentalSystem.Boundaries.UseCases.Identity.Dto.Input;

namespace DentalSystem.Boundaries.Infrastructure.Identity
{
    public interface IJwtAuthManager
    {
        Task<string> GenerateJwtAsync(IUserCredentialsInput userInfo, string roleName);
    }
}