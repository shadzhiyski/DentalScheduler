using System.Collections.Generic;
using System.Linq;
using DentalScheduler.Config.DI;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Infrastructure;
using DentalScheduler.UseCases.Tests.Utilities.DataProviders;
using DentalScheduler.UseCases.Tests.DI;
using DentalScheduler.Web.RestService.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DentalScheduler.Entities.Identity;

namespace DentalScheduler.UseCases.Tests.Utilities
{
    public abstract class BaseIntegrationTests
    {
        public BaseIntegrationTests(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;

            ServiceCollection.RegisterDependencies();
            ServiceCollection.AddTransient<IJwtAuthManager, JwtAuthManager>();
            ServiceCollection.AddTransient<IUserService<User>, UserService>();
            ServiceCollection.AddTransient<IRoleService<IdentityRole>, RoleService>();

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