using DentalScheduler.DAL;
using DentalScheduler.DAL.Repositories;
using DentalScheduler.Interfaces.Gateways;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI
{
    public static class DalRegistrationsExtension
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