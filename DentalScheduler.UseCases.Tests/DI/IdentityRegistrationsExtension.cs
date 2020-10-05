using DentalScheduler.Entities.Identity;
using DentalScheduler.Infrastructure.Common.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases.Tests.DI
{
    public static class IdentityRegistrationsExtension
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;

                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<DentalSchedulerDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}