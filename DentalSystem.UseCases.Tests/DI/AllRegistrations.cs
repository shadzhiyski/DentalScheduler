using System.Reflection;
using DentalSystem.Common.Helpers.Extensions;
using DentalSystem.Infrastructure.Common.Persistence;
using DentalSystem.Infrastructure.Identity;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalSystem.UseCases.Tests.DI
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
                .AddTestDbContext(configuration)
                .AddIdentity(configuration);
    }
}