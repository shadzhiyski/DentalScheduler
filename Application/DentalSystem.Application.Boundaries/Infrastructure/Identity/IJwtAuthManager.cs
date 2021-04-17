using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;

namespace DentalSystem.Application.Boundaries.Infrastructure.Identity
{
    public interface IJwtAuthManager
    {
        Task<string> GenerateJwtAsync(IUserCredentialsInput userInfo, string roleName);
    }
}