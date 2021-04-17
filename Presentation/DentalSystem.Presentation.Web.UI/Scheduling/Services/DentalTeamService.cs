using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.Presentation.Web.UI.Models;
using Simple.OData.Client;

namespace DentalSystem.Presentation.Web.UI.Scheduling.Services
{
    public class DentalTeamService : IDentalTeamService
    {
        public DentalTeamService(ODataClient oDataClient)
        {
            ODataClient = oDataClient;
        }

        public ODataClient ODataClient { get; }

        public async Task<IList<DentalTeamDropDownViewModel>> GetDentalTeamsDropDownListAsync()
            => (await ODataClient
                    .For<DentalTeamDropDownViewModel>("DentalTeam")
                    .FindEntriesAsync()
                )
                .ToList();
    }
}