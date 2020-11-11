using DentalScheduler.Infrastructure.Common.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases.Tests.DI
{
    public static class DbConfigurationRegistrationsExtension
    {
        public static IServiceCollection AddTestDbContext(
            this IServiceCollection services, 
            IConfiguration configuration)
            => services
                .AddScoped<DbContext, DentalSchedulerDbContext>()
                .AddDbContext<DentalSchedulerDbContext>((sp, opt) => 
                    opt.UseSqlite(sp.GetRequiredService<SqliteConnection>())
                )
                .AddScoped<SqliteConnection>(sp => 
                    new SqliteConnection(configuration.GetConnectionString("DentalSchedulerDbConnection"))
                );
    }
}