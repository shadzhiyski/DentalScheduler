using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Common.Helpers.Extensions
{
    public static class DIRegistrationsExtensions
    {
        public static IServiceCollection AddTypes(
            this IServiceCollection services,
            Assembly assembly,
            string typeSuffix)
        {
            var types = assembly
                .GetExportedTypes()
                .Where(t => t.Name.EndsWith(typeSuffix))
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Interface != null)
                .ToList();

            types.ForEach(
                    t => services.AddTransient(t.Interface, t.Implementation)
                );
            
            return services;
        }
    }
}