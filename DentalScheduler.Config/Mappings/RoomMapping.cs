using DentalScheduler.DTO.Output;
using DentalScheduler.DTO.Serialization.Json;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Models.Output;
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

            config.NewConfig<DeserializedRoom, IRoomOutput>()
                .MapWith((src) => new RoomOutput()
                {
                    ReferenceId = src.ReferenceId,
                    Name = src.Name
                });
        }
    }
}