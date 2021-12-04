using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using System.Threading;

namespace DentalSystem.Infrastructure.Scheduling.Persistence.InitialData
{
    public class TreatmentInitialData : IInitialData
    {
        public Type EntityType => typeof(Treatment);

        public int Priority => 7;

        public IEnumerable<object> GetData()
            => new List<Treatment>
            {
                new Treatment() { Name = "Bonding", DurationInMinutes = 45 },
                new Treatment() { Name = "Crowns and Caps", DurationInMinutes = 60 },
                new Treatment() { Name = "Filling And Repair", DurationInMinutes = 45 },
                new Treatment() { Name = "Extraction", DurationInMinutes = 60 },
                new Treatment() { Name = "Bridges and Implants", DurationInMinutes = 120 },
                new Treatment() { Name = "Braces", DurationInMinutes = 60 },
                new Treatment() { Name = "Teeth Whitening", DurationInMinutes = 45 },
                new Treatment() { Name = "Examination", DurationInMinutes = 30 },
            };

        public Task<bool> InitData(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}