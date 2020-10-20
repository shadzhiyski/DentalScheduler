using DentalScheduler.Config.DI.Infrastructure;
using DentalScheduler.Config.DI.UseCases;
using DentalScheduler.Config.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI
{
    public static class AllRegistrations
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
            => services
                .AddInfrastructure()
                .AddUseCases()
                .AddMappings();
    }
}