using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.Web.UI.Models;
using Simple.OData.Client;

namespace DentalSystem.Web.UI.Scheduling.Services
{
    public class TreatmentService : ITreatmentService
    {
        public TreatmentService(ODataClient oDataClient)
        {
            ODataClient = oDataClient;
        }

        public ODataClient ODataClient { get; }

        public async Task<IList<TreatmentDropDownViewModel>> GetTreatmentsAsync()
            => (await ODataClient
                    .For<TreatmentDropDownViewModel>("Treatment")
                    .FindEntriesAsync()
                )
                .ToList();
    }
}