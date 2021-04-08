using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Entities.Scheduling;

namespace DentalSystem.UseCases.Tests.Utilities.DataProviders
{
    public interface ITreatmentDbDataProvider
    {
        Task<IEnumerable<Treatment>> ProvideMainTreatmentsAsync();
    }
}