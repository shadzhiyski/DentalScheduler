using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.Mappings
{
    public static class ApplyMappingsExtension
    {
        public static IServiceCollection ApplyMappings(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<TypeAdapterConfig>();
            config.Apply(
                new AuthResultMapping(),
                new ErrorMapping(),
                new ValidationResultMapping()
            );

            return services;
        }
    }
}