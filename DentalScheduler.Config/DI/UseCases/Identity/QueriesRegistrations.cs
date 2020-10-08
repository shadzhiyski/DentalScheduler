using DentalScheduler.Interfaces.UseCases.Identity.Queries;
using DentalScheduler.UseCases.Identity.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Identity
{
    static class QueriesRegistrations
    {
        public static IServiceCollection RegisterQueries(this IServiceCollection services)
        {
            services.AddTransient<IGetUserProfileQuery, GetUserProfileQuery>();
            
            return services;        
        }
    }
}