using DentalScheduler.Config.DI.Infrastructure;
using DentalScheduler.Config.DI.UseCases;
using DentalScheduler.Config.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI
{
    public static class AllRegistrations
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
            => services
                .RegisterInfrastructure()
                .RegisterUseCases()
                .RegisterMappings();
    }
}