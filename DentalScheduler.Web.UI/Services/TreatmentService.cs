using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalScheduler.Web.UI.Models;
using Simple.OData.Client;

namespace DentalScheduler.Web.UI.Services
{
    public class TreatmentService : ITreatmentService
    {
        public TreatmentService(ODataClient oDataClient)
        {
            ODataClient = oDataClient;
        }

        public ODataClient ODataClient { get; }

        public async Task<IList<TreatmentDropDownViewModel>> GetTreatmentsAsync()
        {
            var result = await ODataClient
                .For<TreatmentDropDownViewModel>("Treatment")
                .FindEntriesAsync();

            return result.ToList();
        }
    }
}