using System.Linq;
using System.Reflection;
using DentalScheduler.Common.Helpers.Extensions;
using DentalScheduler.Interfaces.UseCases.Common.Validation;
using DentalScheduler.UseCases.Common.Mappings;
using DentalScheduler.UseCases.Common.Validation;
using DentalScheduler.UseCases.Scheduling.Mappings;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases
{
    public static class AllRegistrations
    {
        public static readonly Assembly CurrentAssembly = 
            Assembly.GetAssembly(typeof(AllRegistrations));
        
        public static IServiceCollection AddUseCases(this IServiceCollection services)
            => services
                .AddTypes(CurrentAssembly, "Command")
                .AddTypes(CurrentAssembly, "Query")
                .AddValidation(CurrentAssembly)
                .AddMappings();

        public static IServiceCollection AddLightUseCases(this IServiceCollection services)
            => services
                .AddBasicValidation(CurrentAssembly)
                .AddMappings();

        public static IServiceCollection AddBasicValidation(
            this IServiceCollection services,
            Assembly assembly)
        {
            var types = assembly
                .GetExportedTypes()
                .Where(t => t.Name.EndsWith("Validator"))
                .Where(t => t != typeof(ImageValidator))
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    AbstractClass = t.BaseType,
                    Implementation = t
                })
                .Where(t => t.AbstractClass != null)
                .ToList();

            types.ForEach(t => services.AddTransient(t.AbstractClass, t.Implementation));

            services.AddTransient<ImageValidator>();
            services.AddTransient(typeof(IApplicationValidator<>), typeof(ApplicationValidator<>));

            return services;
        }

        private static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();
            services.AddSingleton<TypeAdapterConfig>(config);

            config.Apply(
                new ErrorMapping(),
                new RoomMapping(),
                new DentalTeamMapping(),
                new TreatmentSessionMapping(),
                new CommonMappings(),
                new TreatmentMapping(),
                new PatientMapping()
            );

            return services;
        }

        private static IServiceCollection AddValidation(
            this IServiceCollection services,
            Assembly assembly)
            => services.AddBasicValidation(assembly);
    }
}