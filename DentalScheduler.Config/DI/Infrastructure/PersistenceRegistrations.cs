using DentalScheduler.Infrastructure.Common.Persistence;
using DentalScheduler.Infrastructure.Common.Persistence.Repositories;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.Infrastructure
{
    static class PersistenceRegistrations
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
            => services
                .AddScoped<DbContext, DentalSchedulerDbContext>()
                .AddScoped<DentalSchedulerDbContext>()
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}