using System.Collections.Generic;
using System.Linq;
using DentalScheduler.Config.DI;
using DentalScheduler.Entities;
using DentalScheduler.UseCases.Tests.Utilities.DataProviders;
using DentalScheduler.UseCases.Tests.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DentalScheduler.Infrastructure.Common.Persistence;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.Sqlite;

namespace DentalScheduler.UseCases.Tests.Utilities
{
    public abstract class BaseIntegrationTests : IDisposable
    {
        public BaseIntegrationTests(IServiceCollection serviceCollection)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            ServiceCollection = serviceCollection;

            ServiceCollection.RegisterDependencies();

            ServiceCollection.AddSingleton(typeof(ILogger<>), typeof(Fakes.FakeLogger<>));

            ServiceCollection.AddIdentity();

            ServiceCollection.ConfigureDatabase(Configuration);

            ServiceCollection.RegisterTestDbDataProviders();

            ServiceProvider = ServiceCollection.BuildServiceProvider();

            DbConnection = ServiceProvider.GetRequiredService<SqliteConnection>();

            InitDatabase();
            
            InitMainData();
        }

        public IConfiguration Configuration { get; }

        public IServiceCollection ServiceCollection { get; }

        public ServiceProvider ServiceProvider { get; }

        public SqliteConnection DbConnection { get; }
        
        public IReadOnlyCollection<Treatment> Treatments { get; private set; }

        public void InitDatabase()
        {
            DbConnection.Open();

            ServiceProvider.GetRequiredService<DentalSchedulerDbContext>()
                .Database.EnsureCreated();
        }

        private void InitMainData()
        {
            var treatmentDataProvider = ServiceProvider.GetRequiredService<ITreatmentDbDataProvider>();

            Treatments = treatmentDataProvider.ProvideMainTreatmentsAsync()
                .GetAwaiter()
                .GetResult()
                .ToList();

            var userDbDataProvider = ServiceProvider.GetRequiredService<IUserDbDataProvider>();

            userDbDataProvider.ProvideRolesAsync("Admin", "Dentist", "Patient")
                .GetAwaiter()
                .GetResult();
        }

        public void Dispose()
        {
            DbConnection.Close();

            ServiceProvider.GetRequiredService<DentalSchedulerDbContext>()
                .Database.EnsureDeleted();
        }
    }
}