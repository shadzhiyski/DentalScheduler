namespace DentalSystem.Web.Api
{
    using System.Threading.Tasks;
    using DentalSystem.Interfaces.Infrastructure.Common.Persistence;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Authorization Initialization.
    /// </summary>
    public static class ApplicationInitialization
    {
        /// <summary>
        /// Executes all initializers.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var initializers = serviceScope.ServiceProvider.GetServices<IInitializer>();

            foreach (var initializer in initializers)
            {
                initializer.Initialize()
                    .GetAwaiter()
                    .GetResult();
            }

            return app;
        }
    }
}