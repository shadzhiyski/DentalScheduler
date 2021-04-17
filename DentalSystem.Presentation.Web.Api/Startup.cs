using System;
using DentalSystem.Config.DI.Infrastructure;
using DentalSystem.Application.UseCases;
using DentalSystem.Presentation.Web.Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DentalSystem.Presentation.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddInfrastructure(Configuration)
                .AddUseCases()
                .AddTransient(typeof(Lazy<>), typeof(Lazy<>))
                .AddAuthorization()
                .AddLocalizationMiddleware()
                .AddODataMiddleware()
                .AddSwaggerMiddleware()
                .AddControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            (env.IsDevelopment()
                    ? app.UseDeveloperExceptionPage()
                    : app
                )
                .Initialize()
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseLocalizationMiddleware()
                .UseSwaggerMiddleware()
                .UseCorsMiddleware()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
    }
}
