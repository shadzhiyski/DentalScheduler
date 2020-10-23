using DentalScheduler.Infrastructure.Common.Persistence;
using DentalScheduler.Infrastructure.Common.Persistence.Repositories;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.Infrastructure.Common
{
    static class PersistenceRegistrations
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<DbContext, DentalSchedulerDbContext>()
                .AddScoped<DentalSchedulerDbContext>()
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddDbContext<DentalSchedulerDbContext>(opt =>
                    opt.UseNpgsql(configuration.GetConnectionString("DentalSchedulerDbConnection"))
                );
    }
}