using DentalScheduler.Config.DI.Infrastructure.Common;
using DentalScheduler.Config.DI.Infrastructure.Identity;
using DentalScheduler.Config.DI.UseCases;
using DentalScheduler.Config.Mappings;
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
                .AddUseCases()
                .AddMappings();

        private static IServiceCollection AddTestInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddServices()
                .ConfigureDatabase(configuration)
                .AddIdentity(configuration);
    }
}