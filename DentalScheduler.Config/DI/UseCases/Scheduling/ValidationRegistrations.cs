using DentalScheduler.Interfaces.Dto.Input;
using DentalScheduler.UseCases.Scheduling.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Scheduling
{
    static class ValidationRegistrations
    {
        public static IServiceCollection RegisterValidation(this IServiceCollection services)
        {
            services.AddTransient<AbstractValidator<ITreatmentSessionInput>, TreatmentSessionValidator>();
            
            return services;        
        }
    }
}