using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace DentalSystem.Common.Helpers.Extensions
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
                .Select(t =>
                (
                    Name: t.IsGenericType
                        ? t.Name.Substring(0, t.Name.IndexOf('`'))
                        : t.Name,
                    Type: t
                ))
                .ToDictionary(t => t.Name, t => t.Type);

            var implementationsByName = implementationsAssembly
                .GetExportedTypes()
                .Select(t =>
                (
                    Name: t.IsGenericType
                        ? t.Name.Substring(0, t.Name.IndexOf('`'))
                        : t.Name,
                    Type: t
                ))
                .Where(t => abstractionsByName.ContainsKey($"I{t.Name}"))
                .ToDictionary(t => t.Name, t => t.Type);

            implementationsByName
                .Select(type =>
                (
                    Abstraction: type.Value.IsGenericType
                        ? abstractionsByName[$"I{type.Key}"]
                        : type
                            .Value
                            .GetInterfaces()
                            .Single(i => i.Name.StartsWith($"I{type.Key}")),
                    Implementation: type.Value
                ))
                .ToList()
                .ForEach(dependencyPair => services.AddTransient(dependencyPair.Abstraction, dependencyPair.Implementation));

            return services;
        }

        public static IServiceCollection AddTypes(
            this IServiceCollection services,
            Type abstractionType,
            Assembly implementationsAssembly)
        {
            var allTypes = implementationsAssembly
                .GetExportedTypes();
            var typesFromAssembly = allTypes
                .Where(type => type.IsAssignableTo(abstractionType))
                .ToList();

            typesFromAssembly
                .ForEach(type => services.AddTransient(abstractionType, type));

            return services;
        }

        public static IServiceCollection AddMappings(
            this IServiceCollection services,
            Assembly assembly)
        {
            var config = new TypeAdapterConfig();
            services.AddSingleton<TypeAdapterConfig>(config);

            assembly
                .GetExportedTypes()
                .Where(t => t.Namespace.Contains("Mappings"))
                .Where(t => t.GetInterface(nameof(IRegister)) != null)
                .ToList()
                .ForEach(
                    t => config.Apply(
                        Activator.CreateInstance(t, false) as IRegister
                    )
                );

            return services;
        }
    }
}