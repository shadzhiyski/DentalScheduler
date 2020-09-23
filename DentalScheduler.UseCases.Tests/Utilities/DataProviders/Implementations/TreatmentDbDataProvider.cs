using System.Collections.Generic;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Infrastructure.Persistence;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public class TreatmentDbDataProvider : ITreatmentDbDataProvider
    {
        public TreatmentDbDataProvider(
            IGenericRepository<Treatment> repository,
            IUnitOfWork uoW)
        {
            Repository = repository;
            UoW = uoW;
        }

        public IGenericRepository<Treatment> Repository { get; }
        
        public IUnitOfWork UoW { get; }

        public IEnumerable<Treatment> ProvideMainTreatments()
        {
            var treatments = new List<Treatment>()
            {
                new Treatment
                {
                    Name = "Bonding",
                    DurationInMinutes = 45
                },
                new Treatment
                {
                    Name = "Filling and Repair",
                    DurationInMinutes = 45
                },
                new Treatment
                {
                    Name = "Examination",
                    DurationInMinutes = 30
                },
                new Treatment
                {
                    Name = "Bridges",
                    DurationInMinutes = 120
                }
            };

            foreach (var treatment in treatments)
            {
                Repository.Add(treatment);
            }

            UoW.Save();

            return treatments;
        }
    }
}