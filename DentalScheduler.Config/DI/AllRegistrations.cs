using DentalScheduler.Config.DI.Infrastructure;
using DentalScheduler.Config.DI.UseCases;
using DentalScheduler.Config.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI
{
    public static class AllRegistrations
    {
        public static IServiceCollection AddDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddInfrastructure(configuration)
                .AddUseCases()
                .AddMappings();

        public static IServiceCollection AddLightDependencies(this IServiceCollection services)
            => services
                .AddLightUseCases()
                .AddMappings();
    }
}