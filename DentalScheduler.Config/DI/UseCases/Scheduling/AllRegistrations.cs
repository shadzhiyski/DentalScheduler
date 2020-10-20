using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Scheduling
{
    public static class AllRegistrations
    {
        public static IServiceCollection RegisterScheduling(this IServiceCollection services)
            => services
                .RegisterCommands()
                .RegisterValidation();
    }
}