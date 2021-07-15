using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Infrastructure.Common.Persistence;
using DentalSystem.Application.Boundaries.Infrastructure.Common;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using System.Threading;

namespace DentalSystem.Application.UseCases.Tests.Common
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

        public async Task Initialize(CancellationToken cancellationToken)
        {
            foreach (var initialDataProvider in this.initialDataProviders)
            {
                var applyData = await initialDataProvider.InitData(cancellationToken);

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