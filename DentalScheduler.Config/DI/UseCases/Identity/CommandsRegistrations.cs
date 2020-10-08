using DentalScheduler.Interfaces.UseCases.Identity.Commands;
using DentalScheduler.UseCases.Identity.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Identity
{
    static class CommandsRegistrations
    {
        public static IServiceCollection RegisterCommands(this IServiceCollection services)
        {
            services.AddTransient<ILoginCommand, LoginCommand>();
            services.AddTransient<IRegisterUserCommand, RegisterUserCommand>();
            services.AddTransient<ICreateRoleCommand, CreateRoleCommand>();
            services.AddTransient<ILinkUserAndRoleCommand, LinkUserAndRoleCommand>();
            services.AddTransient<IUpdateProfileCommand, UpdateProfileCommand>();
            
            return services;   
        }
    }
}