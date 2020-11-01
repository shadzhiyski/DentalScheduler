using DentalScheduler.Infrastructure.Common.Config.DI;
using DentalScheduler.Infrastructure.Identity.Config.DI;
using DentalScheduler.Config.Mappings;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.Infrastructure
{
    public static class AllRegistrations
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddCommon(configuration)
                .AddIdentity(configuration)
                .AddMappings();

        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();
            services.AddSingleton<TypeAdapterConfig>(config);

            config.Apply(new AuthResultMapping());

            return services;
        }
    }
}