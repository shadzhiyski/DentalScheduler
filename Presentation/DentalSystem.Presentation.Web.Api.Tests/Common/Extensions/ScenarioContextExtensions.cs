using TechTalk.SpecFlow;

namespace DentalSystem.Presentation.Web.Api.Tests.Common.Extensions
{
    public static class ScenarioContextExtensions
    {
        public static T GetValueOrDefault<T>(this ScenarioContext context, string key, T defaultValue)
            => context.TryGetValue<T>(key, out T value) ? value : defaultValue;
    }
}