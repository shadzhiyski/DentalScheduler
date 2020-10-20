using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Scheduling
{
    internal static class AllRegistrations
    {
        public static IServiceCollection AddScheduling(this IServiceCollection services)
            => services
                .AddCommands()
                .AddValidation();
    }
}