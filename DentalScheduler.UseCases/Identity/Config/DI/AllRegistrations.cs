using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases.Identity.Config.DI
{
    internal static class AllRegistrations
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
            => services
                .AddCommands()
                .AddQueries()
                .AddValidation();

        public static IServiceCollection AddLightIdentity(this IServiceCollection services)
            => services
                .AddBasicValidation();
    }
}