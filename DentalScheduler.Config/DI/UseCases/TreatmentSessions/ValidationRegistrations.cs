using DentalScheduler.Interfaces.Dto.Input;
using DentalScheduler.UseCases.TreatmentSessions.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.TreatmentSessions
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