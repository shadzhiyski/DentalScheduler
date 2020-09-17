using System.Collections.Generic;
using DentalScheduler.Entities;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public interface ITreatmentDbDataProvider
    {
        IEnumerable<Treatment> ProvideMainTreatments();
    }
}