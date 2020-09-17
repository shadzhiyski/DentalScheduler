using DentalScheduler.Entities;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public interface IRoomDbDataProvider
    {
        Room ProvideRoom(string name);
    }
}