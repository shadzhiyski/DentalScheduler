using DentalScheduler.Config.Mappings;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.RegisterMappingsDependencies();
            
            services.RegisterDalDependencies();

            services.RegisterIdentityDependencies();

            services.RegisterValidationDependencies();

            services.RegisterUseCasesDependencies();

            return services;
        }
    }
}