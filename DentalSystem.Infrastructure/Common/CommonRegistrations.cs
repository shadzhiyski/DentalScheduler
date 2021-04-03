using System.Linq;
using DentalSystem.Infrastructure.Common.Persistence;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentalSystem.Infrastructure.Common
{
    internal static class CommonRegistrations
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
                .AddScoped<DbContext, DentalSystemDbContext>()
                .AddDbContext<DentalSystemDbContext>(opt =>
                    opt.UseNpgsql(configuration.GetConnectionString("DentalSystemDbConnection"))
                );
    }
}