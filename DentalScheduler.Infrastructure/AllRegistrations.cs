using DentalScheduler.Common.Helpers.Extensions;
using DentalScheduler.Infrastructure.Common;
using DentalScheduler.Infrastructure.Identity;
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
                .AddMappings(assembly: CurrentAssembly);
    }
}