using DentalScheduler.Interfaces.UseCases.Scheduling.Commands;
using DentalScheduler.UseCases.Scheduling.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Scheduling
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