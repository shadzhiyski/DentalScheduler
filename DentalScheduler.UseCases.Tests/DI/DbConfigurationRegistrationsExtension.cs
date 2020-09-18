using DentalScheduler.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases.Tests.DI
{
    public static class DbConfigurationRegistrationsExtension
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services)
            => services.AddEntityFrameworkNpgsql()
                .AddDbContext<DentalSchedulerDbContext>(opt => 
                    opt.UseInMemoryDatabase("DentalScheduler_test")
                );
    }
}