using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.Infrastructure
{
    public static class AllRegistrations
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            => services
                .AddIdentity()
                .AddPersistence();
    }
}