using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DentalSystem.Infrastructure.Common.Persistence;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.Sqlite;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;

namespace DentalSystem.Application.UseCases.Tests.Common
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
            var treatmentDataProvider = ServiceProvider.GetRequiredService<IInitializer>();
            treatmentDataProvider.Initialize().GetAwaiter().GetResult();

            Treatments = ServiceProvider.GetRequiredService<IGenericRepository<Treatment>>()
                .AsNoTracking()
                .ToList();
        }

        public void Dispose()
        {
            DbConnection.Close();

            ServiceProvider.GetRequiredService<DentalSystemDbContext>()
                .Database.EnsureDeleted();
        }
    }
}