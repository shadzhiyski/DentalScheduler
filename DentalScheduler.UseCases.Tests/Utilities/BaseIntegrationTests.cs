using System.Collections.Generic;
using System.Linq;
using DentalScheduler.Config.DI;
using DentalScheduler.Entities;
using DentalScheduler.UseCases.Tests.Utilities.DataProviders;
using DentalScheduler.UseCases.Tests.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DentalScheduler.UseCases.Tests.Utilities
{
    public abstract class BaseIntegrationTests
    {
        public BaseIntegrationTests(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;

            ServiceCollection.RegisterDependencies();

            ServiceCollection.AddSingleton(typeof(ILogger<>), typeof(Fakes.FakeLogger<>));

            ServiceCollection.AddIdentity();

            ServiceCollection.ConfigureDatabase();

            ServiceCollection.RegisterTestDbDataProviders();

            ServiceProvider = ServiceCollection.BuildServiceProvider();

            InitMainData();
        }

        public IServiceCollection ServiceCollection { get; }

        public ServiceProvider ServiceProvider { get; }

        public IReadOnlyCollection<Treatment> Treatments { get; private set; }

        private void InitMainData()
        {
            var treatmentDataProvider = ServiceProvider.GetRequiredService<ITreatmentDbDataProvider>();

            Treatments = treatmentDataProvider.ProvideMainTreatments().ToList();
        }
    }
}