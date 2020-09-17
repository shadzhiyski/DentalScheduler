using DentalScheduler.Entities;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public interface IDentalTeamDbDataProvider
    {
        DentalTeam ProvideDentalTeam(string teamName, string roomName, params string[] dentistsUserNames);
    }
}