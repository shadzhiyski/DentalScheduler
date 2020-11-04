using DentalScheduler.Common.Helpers.Extensions;
using DentalScheduler.Infrastructure.Common;
using DentalScheduler.Infrastructure.Identity;
using DentalScheduler.Infrastructure.Identity.Mappings;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DentalScheduler.Interfaces.Infrastructure.Common.Persistence;

namespace DentalScheduler.Config.DI.Infrastructure
{
    public static class AllRegistrations
    {
        public static readonly Assembly CurrentAssembly = 
            Assembly.GetAssembly(typeof(AllRegistrations));

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddTypes(
                    abstractionsAssembly: Assembly.GetAssembly(typeof(IUnitOfWork)),
                    implementationsAssembly: CurrentAssembly
                )
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