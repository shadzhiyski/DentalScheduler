using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Output;
using DentalScheduler.Web.UI.Models;

namespace DentalScheduler.Web.UI.Services
{
    public interface IDentalTeamService
    {
        Task<IList<DentalTeamDropDownViewModel>> GetDentalTeamsDropDownListAsync();
    }
}