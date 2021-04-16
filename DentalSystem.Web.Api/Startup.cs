using System;
using DentalSystem.Config.DI.Infrastructure;
using DentalSystem.UseCases;
using DentalSystem.UseCases.Scheduling.Dto.Output;
using DentalSystem.Web.Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace DentalSystem.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddInfrastructure(Configuration)
                .AddUseCases();

            services.AddLocalizationMiddleware();

            services.AddTransient(typeof(Lazy<>), typeof(Lazy<>));
            services.AddControllers();

            services.AddAuthorization();

            services.AddODataMiddleware();

            services.AddSwaggerMiddleware();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Initialize();

            app.UseLocalizationMiddleware();

            app.UseHttpsRedirection();

            app.UseSwaggerMiddleware();

            app.UseCorsMiddleware();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private IEdmModel GetEdmModel()
        {
            ODataModelBuilder odataBuilder = new ODataConventionModelBuilder();

            odataBuilder.EntitySet<RoomOutput>("Room")
                .EntityType.HasKey(e => e.ReferenceId);

            odataBuilder.EntitySet<DentalTeamOutput>("DentalTeam")
                .EntityType.HasKey(e => e.ReferenceId);

            var ts = odataBuilder.EntitySet<TreatmentSessionOutput>("TreatmentSession")
                .EntityType.HasKey(e => e.ReferenceId);

            odataBuilder.EntitySet<TreatmentOutput>("Treatment")
                .EntityType.HasKey(e => e.ReferenceId);

            odataBuilder.EntitySet<PatientOutput>("Patient")
                .EntityType.HasKey(e => e.ReferenceId);

            return odataBuilder.GetEdmModel();
        }
    }
}
