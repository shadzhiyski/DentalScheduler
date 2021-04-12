using System.Collections.Generic;
using System.Linq;
using DentalSystem.UseCases.Tests.Utilities.DataProviders;
using DentalSystem.UseCases.Tests.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DentalSystem.Infrastructure.Common.Persistence;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.Sqlite;
using DentalSystem.Entities.Scheduling;

namespace DentalSystem.UseCases.Tests.Utilities
{
    public abstract class BaseIntegrationTests : BaseTests, IDisposable
    {
        public BaseIntegrationTests(IServiceCollection serviceCollection)
            : base()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            ServiceCollection.AddDependencies(Configuration);

            ServiceCollection.AddSingleton(typeof(ILogger<>), typeof(Fakes.FakeLogger<>));

            ServiceCollection.RegisterTestDbDataProviders();

            ServiceProvider = ServiceCollection.BuildServiceProvider();

            DbConnection = ServiceProvider.GetRequiredService<SqliteConnection>();

            InitDatabase();

            InitMainData();
        }

        public IConfiguration Configuration { get; }

        public SqliteConnection DbConnection { get; }

        public IReadOnlyCollection<Treatment> Treatments { get; private set; }

        public void InitDatabase()
        {
            DbConnection.Open();

            ServiceProvider.GetRequiredService<DentalSystemDbContext>()
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

            ServiceProvider.GetRequiredService<DentalSystemDbContext>()
                .Database.EnsureDeleted();
        }
    }
}