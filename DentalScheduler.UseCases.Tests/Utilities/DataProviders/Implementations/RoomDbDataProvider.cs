using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Gateways;

namespace DentalScheduler.UseCases.Tests.Utilities.DataProviders
{
    public class RoomDbDataProvider : IRoomDbDataProvider
    {
        public RoomDbDataProvider(
            IGenericRepository<Room> repository,
            IUnitOfWork uoW)
        {
            Repository = repository;
            UoW = uoW;
        }

        public IGenericRepository<Room> Repository { get; }

        public IUnitOfWork UoW { get; }
        
        public Room ProvideRoom(string name)
        {
            var room = new Room()
            {
                Name = name
            };

            Repository.Add(room);

            UoW.Save();

            return room;
        }
    }
}