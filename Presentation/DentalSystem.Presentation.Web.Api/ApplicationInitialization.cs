namespace DentalSystem.Presentation.Web.Api
{
    using System.Threading.Tasks;
    using DentalSystem.Application.Boundaries.Infrastructure.Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationInitialization
    {
        public static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var initializers = serviceScope.ServiceProvider.GetServices<IInitializer>();

            foreach (var initializer in initializers)
            {
                initializer.Initialize(default)
                    .GetAwaiter()
                    .GetResult();
            }

            return app;
        }
    }
}