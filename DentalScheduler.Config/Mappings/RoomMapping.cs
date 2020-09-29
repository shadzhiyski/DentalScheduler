using DentalScheduler.Dto.Output;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Dto.Output;
using Mapster;

namespace DentalScheduler.Config.Mappings
{
    public class RoomMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Room, IRoomOutput>()
                .MapWith((src) => new RoomOutput()
                {
                    ReferenceId = src.ReferenceId,
                    Name = src.Name
                });
        }
    }
}