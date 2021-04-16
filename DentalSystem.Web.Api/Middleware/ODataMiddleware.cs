using System.Linq;
using DentalSystem.UseCases.Scheduling.Dto.Output;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace DentalSystem.Web.Api.Middleware
{
    public static class ODataMiddleware
    {
        public static IServiceCollection AddODataMiddleware(this IServiceCollection services)
        {
            services.AddOData(
                opt => opt.AddModel(prefix: "odata", model: GetEdmModel())
                    .Select().Filter().Expand().OrderBy().Count().SetMaxTop(1000).SkipToken()
            );

            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });

            return services;
        }

        private static IEdmModel GetEdmModel()
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