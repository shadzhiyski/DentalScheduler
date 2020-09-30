using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.Mappings
{
    public static class ApplyMappingsExtension
    {
        public static IServiceCollection RegisterMappings(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();
            services.AddSingleton<TypeAdapterConfig>(config);

            config.Apply(
                new AuthResultMapping(),
                new ErrorMapping(),
                new RoomMapping(),
                new DentalTeamMapping(),
                new TreatmentSessionMapping(),
                new CommonMappings(),
                new TreatmentMapping()
            );

            return services;
        }
    }
}