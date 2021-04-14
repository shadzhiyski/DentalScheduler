using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Infrastructure.Common.Persistence;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;

namespace DentalSystem.UseCases.Tests.Utilities
{
    internal class TestDatabaseInitializer : IInitializer
    {
        private readonly DentalSystemDbContext db;
        private readonly IEnumerable<IInitialData> initialDataProviders;

        public TestDatabaseInitializer(
            DentalSystemDbContext db,
            IEnumerable<IInitialData> initialDataProviders)
        {
            this.db = db;
            this.initialDataProviders = initialDataProviders;
        }

        public async Task Initialize()
        {
            foreach (var initialDataProvider in this.initialDataProviders)
            {
                var applyData = await initialDataProvider.InitData();

                if (applyData)
                {
                    var data = initialDataProvider.GetData();
                    this.db.AddRange(data);
                }
            }

            this.db.SaveChanges();
        }
    }
}