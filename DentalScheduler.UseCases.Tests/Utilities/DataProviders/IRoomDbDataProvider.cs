using System.Threading.Tasks;
using DentalScheduler.Entities;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public interface IRoomDbDataProvider
    {
        Task<Room> ProvideRoom(string name);
    }
}