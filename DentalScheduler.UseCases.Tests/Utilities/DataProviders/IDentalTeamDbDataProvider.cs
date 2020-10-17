using System.Threading.Tasks;
using DentalScheduler.Entities;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public interface IDentalTeamDbDataProvider
    {
        Task<DentalTeam> ProvideDentalTeam(string teamName, string roomName, params string[] dentistsUserNames);
    }
}