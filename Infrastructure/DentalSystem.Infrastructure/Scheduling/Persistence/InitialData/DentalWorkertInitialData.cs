using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using System.Threading;

namespace DentalSystem.Infrastructure.Scheduling.Persistence.InitialData
{
    public class DentalWorkertInitialData : IInitialData
    {
        public Type EntityType => typeof(DentalWorker);

        public int Priority => 3;

        public IEnumerable<object> GetData()
            => new List<DentalWorker>
            {
                new DentalWorker
                {
                    Id = new Guid("3fb71697-6ae3-4300-b172-e64d580e3ef0"),
                    IdentityUserId = "0a617280-ed83-4447-93ba-f8ed7aaae62b",
                    JobType = JobType.Dentist
                },
                new DentalWorker
                {
                    Id = new Guid("3fc6d593-e172-433f-ac86-af578d6a81e8"),
                    IdentityUserId = "3c9e331e-b7c7-4667-8615-b71b9e4187f1",
                    JobType = JobType.Dentist
                },
                new DentalWorker
                {
                    Id = new Guid("73b047e2-e5cd-456a-8c89-bb573dad0b43"),
                    IdentityUserId = "74da05e6-876e-4457-823d-440e5c1686cd",
                    JobType = JobType.DentistAssistant
                },
                new DentalWorker
                {
                    Id = new Guid("16e3487c-f39d-4bb0-ba5d-eec5db17ad5b"),
                    IdentityUserId = "d72bb325-d935-4da3-bb7d-fdd748c9878a",
                    JobType = JobType.DentistAssistant
                },
            };

        public Task<bool> InitData(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}