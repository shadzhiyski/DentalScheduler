using DentalScheduler.Interfaces.UseCases.TreatmentSession;
using DentalScheduler.Interfaces.UseCases.Identity;
using DentalScheduler.UseCases.TreatmentSessions;
using DentalScheduler.UseCases.Identity;
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
            services.AddTransient<IAddTreatmentSessionCommand, AddTreatmentSessionCommand>();
            services.AddTransient<IUpdateTreatmentSessionCommand, UpdateTreatmentSessionCommand>();
            
            return services;
        }
    }
}