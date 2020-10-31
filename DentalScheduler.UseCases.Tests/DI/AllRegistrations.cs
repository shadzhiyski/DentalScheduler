using DentalScheduler.Infrastructure.Common.Config.DI;
using DentalScheduler.Infrastructure.Identity.Config.DI;
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
                .AddServices()
                .AddTestDbContext(configuration)
                .AddIdentity(configuration);
    }
}