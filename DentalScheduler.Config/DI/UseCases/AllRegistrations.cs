using DentalScheduler.Config.DI.UseCases.Common;
using DentalScheduler.Config.DI.UseCases.Identity;
using DentalScheduler.Config.DI.UseCases.Scheduling;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases
{
    public static class AllRegistrations
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
            => services
                .AddCommon()
                .AddIdentity()
                .AddScheduling();
    }
}