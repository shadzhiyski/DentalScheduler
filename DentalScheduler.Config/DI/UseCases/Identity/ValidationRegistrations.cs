using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using DentalScheduler.UseCases.Identity.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Identity
{
    static class ValidationRegistrations
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
            => services
                .AddBasicValidation();

        public static IServiceCollection AddBasicValidation(this IServiceCollection services)
            => services
                .AddTransient<AbstractValidator<ILinkUserAndRoleInput>, LinkUserAndRoleValidator>()
                .AddTransient<AbstractValidator<ICreateRoleInput>, CreateRoleValidator>()
                .AddTransient<AbstractValidator<IUserCredentialsInput>, UserCredentialsValidator>()
                .AddTransient<AbstractValidator<IUserProfileInput>, UserProfileValidator>();
    }
}