using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.Entities;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public interface ITreatmentDbDataProvider
    {
        Task<IEnumerable<Treatment>> ProvideMainTreatments();
    }
}