using DentalScheduler.Interfaces.UseCases.Scheduling.Commands;
using DentalScheduler.UseCases.Scheduling.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases.Scheduling.Config.DI
{
    static class CommandsRegistrations
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
            => services
                .AddTransient<IAddTreatmentSessionCommand, AddTreatmentSessionCommand>()
                .AddTransient<IUpdateTreatmentSessionCommand, UpdateTreatmentSessionCommand>();
    }
}