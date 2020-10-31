using DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Input;
using DentalScheduler.UseCases.Scheduling.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases.Scheduling.Config.DI
{
    static class ValidationRegistrations
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
            => services
                .AddBasicValidation();

        public static IServiceCollection AddBasicValidation(this IServiceCollection services)
            => services
                .AddTransient<AbstractValidator<ITreatmentSessionInput>, TreatmentSessionValidator>();
    }
}