using DentalScheduler.Interfaces.Models.Input;

namespace DentalScheduler.Interfaces.Infrastructure.Identity
{
    public interface IJwtAuthManager
    {
         string GenerateJwt(IUserCredentialsInput userInfo, string roleName);
    }
}