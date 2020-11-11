using System.Reflection;
using DentalScheduler.Common.Helpers.Extensions;
using DentalScheduler.Infrastructure.Common.Persistence;
using DentalScheduler.Infrastructure.Identity;
using DentalScheduler.Interfaces.Infrastructure.Common.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases.Tests.DI
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