using DentalScheduler.Infrastructure.Common.Persistence;
using DentalScheduler.Infrastructure.Common.Persistence.Repositories;
using DentalScheduler.Interfaces.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.Infrastructure.Common
{
    internal static class AllRegistrations
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
            => services
                .AddScoped<DbContext, DentalSchedulerDbContext>()
                .AddScoped<DentalSchedulerDbContext>()
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}