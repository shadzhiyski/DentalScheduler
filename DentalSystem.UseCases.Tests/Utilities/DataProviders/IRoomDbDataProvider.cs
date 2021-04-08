using System.Threading.Tasks;
using DentalSystem.Entities.Scheduling;

namespace DentalSystem.UseCases.Tests.Utilities.DataProviders
{
    public interface IRoomDbDataProvider
    {
        Task<Room> ProvideRoomAsync(string name);
    }
}