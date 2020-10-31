using DentalScheduler.Entities.Identity;
using DentalScheduler.Infrastructure.Identity.Services;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Infrastructure.Identity.Config.DI
{
    static class ServicesRegistrations
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services
                .AddTransient<IJwtAuthManager, JwtAuthManager>()
                .AddTransient<IUserService<User>, UserService>()
                .AddTransient<IRoleService<IdentityRole>, RoleService>();
    }
}