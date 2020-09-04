using DentalScheduler.Interfaces.UseCases;
using DentalScheduler.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI
{
    internal static class UserCasesRegistrationsExtension
    {
        public static IServiceCollection RegisterUseCasesDependencies(this IServiceCollection services)
        {
            services.AddTransient<ILoginCommand, LoginCommand>();
            services.AddTransient<IRegisterUserCommand, RegisterUserCommand>();
            services.AddTransient<ICreateRoleCommand, CreateRoleCommand>();
            services.AddTransient<ILinkUserAndRoleCommand, LinkUserAndRoleCommand>();
            
            return services;
        }
    }
}