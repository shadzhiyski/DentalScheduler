using DentalScheduler.Interfaces.UseCases.Identity;
using DentalScheduler.UseCases.Identity;
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