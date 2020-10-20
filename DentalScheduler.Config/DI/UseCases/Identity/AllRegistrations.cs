using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Identity
{
    internal static class AllRegistrations
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
            => services
                .AddCommands()
                .AddQueries()
                .AddValidation();
    }
}