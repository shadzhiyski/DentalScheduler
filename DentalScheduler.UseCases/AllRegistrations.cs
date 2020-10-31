using DentalScheduler.UseCases.Common.Config.DI;
using DentalScheduler.UseCases.Common.Config.Mappings;
using DentalScheduler.UseCases.Identity.Config.DI;
using DentalScheduler.UseCases.Scheduling.Config.DI;
using DentalScheduler.UseCases.Scheduling.Config.Mappings;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases
{
    public static class AllRegistrations
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
            => services
                .AddCommon()
                .AddIdentity()
                .AddScheduling()
                .AddMappings();

        public static IServiceCollection AddLightUseCases(this IServiceCollection services)
            => services
                .AddCommon()
                .AddLightIdentity()
                .AddLightScheduling()
                .AddMappings();

        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();
            services.AddSingleton<TypeAdapterConfig>(config);

            config.Apply(
                new ErrorMapping(),
                new RoomMapping(),
                new DentalTeamMapping(),
                new TreatmentSessionMapping(),
                new CommonMappings(),
                new TreatmentMapping(),
                new PatientMapping()
            );

            return services;
        }
    }
}