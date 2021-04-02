using System.Threading.Tasks;
using DentalSystem.Interfaces.UseCases.Identity.Dto.Input;

namespace DentalSystem.Interfaces.Infrastructure.Identity
{
    public interface IJwtAuthManager
    {
        Task<string> GenerateJwtAsync(IUserCredentialsInput userInfo, string roleName);
    }
}