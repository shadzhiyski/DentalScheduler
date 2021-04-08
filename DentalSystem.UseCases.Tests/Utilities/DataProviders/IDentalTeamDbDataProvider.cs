using System.Threading.Tasks;
using DentalSystem.Entities.Scheduling;

namespace DentalSystem.UseCases.Tests.Utilities.DataProviders
{
    public interface IDentalTeamDbDataProvider
    {
        Task<DentalTeam> ProvideDentalTeamAsync(string teamName, string roomName, params string[] dentistsUserNames);
    }
}