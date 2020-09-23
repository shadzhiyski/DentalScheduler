using DentalScheduler.Entities.Identity;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using DentalScheduler.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI
{
    public static class IdentityRegistrationsExtension
    {
        public static IServiceCollection RegisterIdentityDependencies(this IServiceCollection services)
        {
            services.AddTransient<IJwtAuthManager, JwtAuthManager>();
            services.AddTransient<IUserService<User>, UserService>();
            services.AddTransient<IRoleService<IdentityRole>, RoleService>();

            return services;
        }
    }
}