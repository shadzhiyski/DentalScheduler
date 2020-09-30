using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.Infrastructure
{
    public static class AllRegistrations
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
            => services
                .RegisterIdentity()
                .RegisterPersistence();
    }
}