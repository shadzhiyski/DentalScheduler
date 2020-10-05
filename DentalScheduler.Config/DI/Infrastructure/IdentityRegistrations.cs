using DentalScheduler.Entities.Identity;
using DentalScheduler.Infrastructure.Identity.Services;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.Infrastructure
{
    static class IdentityRegistrations
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            services.AddTransient<IJwtAuthManager, JwtAuthManager>();
            services.AddTransient<IUserService<User>, UserService>();
            services.AddTransient<IRoleService<IdentityRole>, RoleService>();

            return services;
        }
    }
}