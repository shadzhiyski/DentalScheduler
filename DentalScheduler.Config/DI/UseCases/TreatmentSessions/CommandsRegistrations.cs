using DentalScheduler.Interfaces.UseCases.TreatmentSessions;
using DentalScheduler.UseCases.TreatmentSessions;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.TreatmentSessions
{
    static class CommandsRegistrations
    {
        public static IServiceCollection RegisterCommands(this IServiceCollection services)
        {
            services.AddTransient<IAddTreatmentSessionCommand, AddTreatmentSessionCommand>();
            services.AddTransient<IUpdateTreatmentSessionCommand, UpdateTreatmentSessionCommand>();
            
            return services;
        }
    }
}