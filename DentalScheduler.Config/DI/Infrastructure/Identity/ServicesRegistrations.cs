using System;
using System.Text;
using DentalScheduler.Entities.Identity;
using DentalScheduler.Infrastructure.Identity.Services;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.Infrastructure.Identity
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