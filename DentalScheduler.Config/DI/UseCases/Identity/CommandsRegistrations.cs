using DentalScheduler.Interfaces.UseCases.Identity.Commands;
using DentalScheduler.UseCases.Identity.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Identity
{
    static class CommandsRegistrations
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
            => services
                .AddTransient<ILoginCommand, LoginCommand>()
                .AddTransient<IRegisterUserCommand, RegisterUserCommand>()
                .AddTransient<ICreateRoleCommand, CreateRoleCommand>()
                .AddTransient<ILinkUserAndRoleCommand, LinkUserAndRoleCommand>()
                .AddTransient<IUpdateProfileCommand, UpdateProfileCommand>();
    }
}