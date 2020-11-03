using DentalScheduler.Infrastructure.Common.Persistence;
using DentalScheduler.Infrastructure.Common.Persistence.Repositories;
using DentalScheduler.Interfaces.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Infrastructure.Common.Config.DI
{
    public static class PersistenceRegistrations
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext(configuration);

        public static IServiceCollection AddDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<DbContext, DentalSchedulerDbContext>()
                .AddDbContext<DentalSchedulerDbContext>(opt =>
                    opt.UseNpgsql(configuration.GetConnectionString("DentalSchedulerDbConnection"))
                );
    }
}