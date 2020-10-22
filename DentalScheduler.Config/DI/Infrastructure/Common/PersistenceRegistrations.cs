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

namespace DentalScheduler.Config.DI.Infrastructure.Common
{
    static class PersistenceRegistrations
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<DbContext, DentalSchedulerDbContext>()
                .AddScoped<DentalSchedulerDbContext>()
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddDbContext<DentalSchedulerDbContext>(opt =>
                    opt.UseNpgsql(configuration.GetConnectionString("DentalSchedulerDbConnection"),
                            x => x.MigrationsAssembly("DentalScheduler.Infrastructure"))
                );
    }
}