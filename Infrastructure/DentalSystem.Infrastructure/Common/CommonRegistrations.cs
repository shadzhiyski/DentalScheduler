using System.Linq;
using DentalSystem.Infrastructure.Common.Persistence;
using DentalSystem.Application.Boundaries.Infrastructure.Common;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DentalSystem.Infrastructure.Common.Persistence.Repositories;

namespace DentalSystem.Infrastructure.Common
{
    internal static class CommonRegistrations
    {
        public static IServiceCollection AddCommon(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddPersistence(configuration);

        private static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<DbContext, DentalSystemDbContext>()
                .AddDbContext<DentalSystemDbContext>(opt =>
                    opt.UseNpgsql(configuration.GetConnectionString("DentalSystemDbConnection"))
                )
                .AddScoped(typeof(IReadRepository<>), typeof(GenericRepository<>))
                .AddScoped(typeof(IWriteRepository<>), typeof(GenericRepository<>))
                .AddScoped<IInitializer, DatabaseInitializer>(
                    (sp) => new DatabaseInitializer(
                        db: sp.GetService<DentalSystemDbContext>(),
                        initialDataProviders: sp
                            .GetServices<IInitialData>()
                            .OrderBy(id => id.Priority)
                    )
                );
    }
}