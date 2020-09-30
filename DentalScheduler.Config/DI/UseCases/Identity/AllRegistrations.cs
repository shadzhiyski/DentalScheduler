using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Identity
{
    public static class AllRegistrations
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
            => services
                .RegisterCommands()
                .RegisterQueries()
                .RegisterValidation();
    }
}