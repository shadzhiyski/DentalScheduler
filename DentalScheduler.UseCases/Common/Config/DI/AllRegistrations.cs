using DentalScheduler.Interfaces.UseCases.Common.Validation;
using DentalScheduler.UseCases.Common.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases.Common.Config.DI
{
    internal static class AllRegistrations
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
            => services
                .AddTransient<ImageValidator>()
                .AddTransient(typeof(IApplicationValidator<>), typeof(ApplicationValidator<>));
    }
}