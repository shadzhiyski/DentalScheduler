using System.Threading.Tasks;
using DentalSystem.Entities;

namespace DentalSystem.UseCases.Tests.Utilities.DataProviders
{
    public interface IRoomDbDataProvider
    {
        Task<Room> ProvideRoomAsync(string name);
    }
}