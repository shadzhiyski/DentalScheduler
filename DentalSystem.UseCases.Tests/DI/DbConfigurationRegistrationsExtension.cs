using System;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DentalSystem.Infrastructure.Common.Persistence;

namespace DentalSystem.UseCases.Tests.DI
{
    public static class DbConfigurationRegistrationsExtension
    {
        public static IServiceCollection AddTestDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<DbContext, DentalSystemDbContext>()
                .AddScoped<Action<ModelBuilder>>((sp) =>
                    (modelBuilder) =>
                    {
                        var dbContext = sp.GetRequiredService<DentalSystemDbContext>();
                        if (dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
                        {
                            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                            {
                                var properties = entityType.ClrType
                                    .GetProperties()
                                    .Where(p => p.PropertyType == typeof(decimal));
                                foreach (var property in properties)
                                {
                                    modelBuilder
                                        .Entity(entityType.Name)
                                        .Property(property.Name)
                                        .HasConversion<double>();
                                }

                                var dateTimeProperties = entityType.ClrType
                                    .GetProperties()
                                    .Where(p => p.PropertyType == typeof(DateTimeOffset));
                                foreach (var property in dateTimeProperties)
                                {
                                    modelBuilder
                                        .Entity(entityType.Name)
                                        .Property(property.Name)
                                        .HasConversion(new DateTimeOffsetToBinaryConverter());
                                }
                            }
                        }
                    }
                )
                .AddDbContext<DentalSystemDbContext>((sp, opt) =>
                    opt.UseSqlite(sp.GetRequiredService<SqliteConnection>())
                )
                .AddScoped<SqliteConnection>(sp =>
                    new SqliteConnection(configuration.GetConnectionString("DentalSystemDbConnection"))
                );
    }
}