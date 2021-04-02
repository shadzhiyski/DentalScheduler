using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Entities;

namespace DentalSystem.UseCases.Tests.Utilities.DataProviders
{
    public interface ITreatmentDbDataProvider
    {
        Task<IEnumerable<Treatment>> ProvideMainTreatmentsAsync();
    }
}