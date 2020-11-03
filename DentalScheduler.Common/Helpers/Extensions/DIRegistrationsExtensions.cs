using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Common.Helpers.Extensions
{
    public static class DIRegistrationsExtensions
    {
        public static IServiceCollection AddTypes(
            this IServiceCollection services,
            Assembly abstractionsAssembly,
            Assembly implementationsAssembly)
        {
            var abstractionsByName = abstractionsAssembly
                .GetExportedTypes()
                .Where(t => t.IsAbstract)
                .Where(t => !t.Namespace.Contains("Dto"))
                .ToDictionary(t => t.Name, t => t);

            var implementationsByName = implementationsAssembly
                .GetExportedTypes()
                .Where(t => abstractionsByName.ContainsKey($"I{t.Name}"))
                .ToDictionary(t => t.Name, t => t);

            implementationsByName
                .Select(type => 
                (
                    Abstraction: abstractionsByName[$"I{type.Key}"],
                    Implementation: type.Value
                ))
                .ToList()
                .ForEach(dependencyPair => services.AddTransient(dependencyPair.Abstraction, dependencyPair.Implementation));

            return services;
        }
    }
}