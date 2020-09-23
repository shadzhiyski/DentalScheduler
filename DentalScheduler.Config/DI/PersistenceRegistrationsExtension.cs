using DentalScheduler.Infrastructure.Persistence;
using DentalScheduler.Infrastructure.Persistence.Repositories;
using DentalScheduler.Interfaces.Gateways;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI
{
    public static class PersistenceRegistrationsExtension
    {
        public static IServiceCollection RegisterDalDependencies(this IServiceCollection services)
        {
            services.AddScoped<DbContext, DentalSchedulerDbContext>();
            services.AddScoped<DentalSchedulerDbContext>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}