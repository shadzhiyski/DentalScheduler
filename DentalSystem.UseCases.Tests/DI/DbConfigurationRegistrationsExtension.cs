using DentalSystem.Infrastructure.Common.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalSystem.UseCases.Tests.DI
{
    public static class DbConfigurationRegistrationsExtension
    {
        public static IServiceCollection AddTestDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<DbContext, DentalSystemDbContext>()
                .AddDbContext<DentalSystemDbContext>((sp, opt) =>
                    opt.UseSqlite(sp.GetRequiredService<SqliteConnection>())
                )
                .AddScoped<SqliteConnection>(sp =>
                    new SqliteConnection(configuration.GetConnectionString("DentalSystemDbConnection"))
                );
    }
}