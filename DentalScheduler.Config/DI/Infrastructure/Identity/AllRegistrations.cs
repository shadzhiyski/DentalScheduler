using DentalScheduler.Entities.Identity;
using DentalScheduler.Infrastructure.Identity.Services;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.Infrastructure.Identity
{
    internal static class AllRegistrations
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
            => services
                .AddTransient<IJwtAuthManager, JwtAuthManager>()
                .AddTransient<IUserService<User>, UserService>()
                .AddTransient<IRoleService<IdentityRole>, RoleService>();
    }
}