using System.Threading.Tasks;
using DentalScheduler.Entities;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public interface IDentalTeamDbDataProvider
    {
        Task<DentalTeam> ProvideDentalTeamAsync(string teamName, string roomName, params string[] dentistsUserNames);
    }
}