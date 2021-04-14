using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DentalSystem.UseCases.Tests.Common
{
    public class BaseTests
    {
        public BaseTests()
        {
            ServiceCollection = new ServiceCollection();

            ServiceCollection.AddLocalization();

            ServiceCollection.AddSingleton<ILoggerFactory, LoggerFactory>();
            ServiceCollection.AddSingleton(typeof(ILogger<>), typeof(Fakes.FakeLogger<>));

            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        public IServiceCollection ServiceCollection { get; }

        public ServiceProvider ServiceProvider { get; init; }
    }
}