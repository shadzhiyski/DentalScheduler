using DentalScheduler.Interfaces.UseCases.Identity.Queries;
using DentalScheduler.UseCases.Identity.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases.Identity.Config.DI
{
    static class QueriesRegistrations
    {
        public static IServiceCollection AddQueries(this IServiceCollection services)
            => services
                .AddTransient<IGetUserProfileQuery, GetUserProfileQuery>();
    }
}