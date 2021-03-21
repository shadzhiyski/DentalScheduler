using DentalSystem.Common.Helpers.Extensions;
using DentalSystem.Infrastructure.Common;
using DentalSystem.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;

namespace DentalSystem.Config.DI.Infrastructure
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
                .AddTypes(
                    abstractionType: typeof(IInitialData),
                    implementationsAssembly: CurrentAssembly
                )
                .AddCommon(configuration)
                .AddIdentity(configuration)
                .AddMappings(assembly: CurrentAssembly);
    }
}