using System.Threading.Tasks;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;

namespace DentalScheduler.Interfaces.Infrastructure.Identity
{
    public interface IJwtAuthManager
    {
        Task<string> GenerateJwtAsync(IUserCredentialsInput userInfo, string roleName);
    }
}