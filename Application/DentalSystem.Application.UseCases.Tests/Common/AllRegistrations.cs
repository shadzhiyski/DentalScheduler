using System.Linq;
using System.Reflection;
using DentalSystem.Common.Helpers.Extensions;
using DentalSystem.Infrastructure.Common.Persistence;
using DentalSystem.Infrastructure.Identity;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DentalSystem.Infrastructure.Common.Persistence.Repositories;

namespace DentalSystem.Application.UseCases.Tests.Common
{
    public static class AllRegistrations
    {
        public static IServiceCollection AddDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddTestInfrastructure(configuration)
                .AddUseCases();

        private static IServiceCollection AddTestInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddTypes(
                    abstractionsAssembly: Assembly.GetAssembly(typeof(IUnitOfWork)),
                    implementationsAssembly: Assembly.GetAssembly(typeof(UnitOfWork))
                )
                .AddTypes(
                    abstractionType: typeof(IInitialData),
                    implementationsAssembly: Assembly.GetAssembly(typeof(DentalSystemDbContext))
                )
                .AddScoped(typeof(IReadRepository<>), typeof(GenericRepository<>))
                .AddScoped(typeof(IWriteRepository<>), typeof(GenericRepository<>))
                .AddScoped<IInitializer, TestDatabaseInitializer>(
                    (sp) => new TestDatabaseInitializer(
                        db: sp.GetService<DentalSystemDbContext>(),
                        initialDataProviders: sp
                            .GetServices<IInitialData>()
                            .OrderBy(id => id.Priority)
                    )
                )
                .AddTestDbContext(configuration)
                .AddIdentity(configuration);
    }
}