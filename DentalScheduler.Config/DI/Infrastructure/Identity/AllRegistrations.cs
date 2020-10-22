using System;
using System.Text;
using DentalScheduler.Infrastructure.Common.Persistence;
using DentalScheduler.Infrastructure.Common.Persistence.Repositories;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using DentalScheduler.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace DentalScheduler.Config.DI.Infrastructure.Identity
{
    internal static class AllRegistrations
    {
        public static IServiceCollection AddIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddIdentity<User, IdentityRole>(options =>
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

            services
                .AddAuthentication(configuration)
                .AddServices();

            return services;
        }
    }
}