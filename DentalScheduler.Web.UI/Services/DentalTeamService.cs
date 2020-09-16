using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalScheduler.Web.UI.Models;
using Simple.OData.Client;

namespace DentalScheduler.Web.UI.Services
{
    public class DentalTeamService : IDentalTeamService
    {
        public DentalTeamService(ODataClient oDataClient)
        {
            ODataClient = oDataClient;
        }

        public ODataClient ODataClient { get; }

        public async Task<IList<DentalTeamDropDownViewModel>> GetDentalTeamsDropDownListAsync()
        {
            var result = await ODataClient
                .For<DentalTeamDropDownViewModel>("DentalTeam")
                .FindEntriesAsync();

            return result.ToList();
        }
    }
}