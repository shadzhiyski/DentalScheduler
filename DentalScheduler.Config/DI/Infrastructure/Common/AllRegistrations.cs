using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.Infrastructure.Common
{
    internal static class AllRegistrations
    {
        public static IServiceCollection AddCommon(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddPersistence(configuration);
    }
}