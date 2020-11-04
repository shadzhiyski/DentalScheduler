using DentalScheduler.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Infrastructure.Common.Config.DI
{
    internal static class AllRegistrations
    {
        public static IServiceCollection AddCommon(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddPersistence(configuration);
        
        private static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<DbContext, DentalSchedulerDbContext>()
                .AddDbContext<DentalSchedulerDbContext>(opt =>
                    opt.UseNpgsql(configuration.GetConnectionString("DentalSchedulerDbConnection"))
                );
    }
}