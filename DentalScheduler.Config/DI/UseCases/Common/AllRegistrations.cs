using DentalScheduler.Interfaces.UseCases.Common.Validation;
using DentalScheduler.UseCases.Common.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Common
{
    public static class AllRegistrations
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            services.AddTransient<ImageValidator>();
            
            services.AddTransient(typeof(IApplicationValidator<>), typeof(ApplicationValidator<>));
            
            return services;
        }
    }
}