using DentalScheduler.Infrastructure.Common.Persistence;
using DentalScheduler.Infrastructure.Common.Persistence.Repositories;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.Infrastructure
{
    static class PersistenceRegistrations
    {
        public static IServiceCollection RegisterPersistence(this IServiceCollection services)
        {
            services.AddScoped<DbContext, DentalSchedulerDbContext>();
            services.AddScoped<DentalSchedulerDbContext>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}