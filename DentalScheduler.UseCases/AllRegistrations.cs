using System.Linq;
using System.Reflection;
using DentalScheduler.Common.Helpers.Extensions;
using DentalScheduler.Interfaces.UseCases.Common.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.UseCases
{
    public static class AllRegistrations
    {
        public static readonly Assembly CurrentAssembly = 
            Assembly.GetAssembly(typeof(AllRegistrations));
        
        public static IServiceCollection AddUseCases(this IServiceCollection services)
            => services
                .AddTypes(
                    abstractionsAssembly: Assembly.GetAssembly(typeof(IApplicationValidator<>)),
                    implementationsAssembly: CurrentAssembly
                )
                .AddValidation(CurrentAssembly)
                .AddMappings(assembly: CurrentAssembly);

        public static IServiceCollection AddLightUseCases(this IServiceCollection services)
            => services
                .AddBasicValidation(CurrentAssembly)
                .AddMappings(assembly: CurrentAssembly);

        public static IServiceCollection AddBasicValidation(
            this IServiceCollection services,
            Assembly assembly)
        {
            var validatorsTypes = assembly
                .GetExportedTypes()
                .Where(t => t.Name.EndsWith("Validator"))
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.BaseType != null)
                .Select(t => new
                {
                    IsCommon = t.Namespace.Contains("Common"),
                    AbstractClass = t.BaseType,
                    Implementation = t
                })
                .ToList();

            validatorsTypes
                .Where(t => !t.IsCommon)
                .ToList()
                .ForEach(t => services.AddTransient(t.AbstractClass, t.Implementation));
            validatorsTypes
                .ForEach(t => services.AddTransient(t.Implementation));

            return services;
        }

        private static IServiceCollection AddValidation(
            this IServiceCollection services,
            Assembly assembly)
            => services.AddBasicValidation(assembly);
    }
}