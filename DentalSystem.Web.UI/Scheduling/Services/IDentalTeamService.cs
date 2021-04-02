using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Web.UI.Models;

namespace DentalSystem.Web.UI.Scheduling.Services
{
    public interface IDentalTeamService
    {
        Task<IList<DentalTeamDropDownViewModel>> GetDentalTeamsDropDownListAsync();
    }
}