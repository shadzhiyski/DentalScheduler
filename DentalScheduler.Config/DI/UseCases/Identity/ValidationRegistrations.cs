using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using DentalScheduler.UseCases.Identity.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Identity
{
    static class ValidationRegistrations
    {
        public static IServiceCollection RegisterValidation(this IServiceCollection services)
        {
            services.AddTransient<AbstractValidator<ILinkUserAndRoleInput>, LinkUserAndRoleValidator>();
            services.AddTransient<AbstractValidator<ICreateRoleInput>, CreateRoleValidator>();
            services.AddTransient<AbstractValidator<IUserCredentialsInput>, UserCredentialsValidator>();
            services.AddTransient<AbstractValidator<IUserProfileInput>, UserProfileValidator>();
            
            return services;        
        }
    }
}