using DentalScheduler.Config.DI.Infrastructure.Common;
using DentalScheduler.Config.DI.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.Infrastructure
{
    public static class AllRegistrations
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            => services
                .AddCommon()
                .AddIdentity();
    }
}