using DentalScheduler.UseCases.Tests.Utilities.DataProviders;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases.Tests.DI
{
    public static class TestDbDataProvidersRegistrationsExtension
    {
        public static IServiceCollection RegisterTestDbDataProviders(this IServiceCollection services)
        {
            services.AddSingleton<ITreatmentDbDataProvider, TreatmentDbDataProvider>();
            services.AddSingleton<IUserDbDataProvider, UserDbDataProvider>();
            services.AddSingleton<IRoomDbDataProvider, RoomDbDataProvider>();
            services.AddSingleton<IDentalTeamDbDataProvider, DentalTeamDbDataProvider>();

            return services;
        }
    }
}