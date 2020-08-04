using DentalScheduler.Interfaces.Models.Input;

namespace DentalScheduler.Interfaces.Infrastructure
{
    public interface IJwtAuthManager
    {
         string GenerateJwt(IUserCredentialsInput userInfo, string roleName);
    }
}