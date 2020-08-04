using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.Mappings
{
    public static class ApplyMappingsExtension
    {
        public static IServiceCollection ApplyMappings(this IServiceCollection services)
        {

            TypeAdapterConfig.GlobalSettings.Apply(
                new AuthResultMapping(),
                new ErrorMapping(),
                new ValidationErrorMapping(),
                new ValidationResultMapping()
            );

            return services;
        }
    }
}