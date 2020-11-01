using DentalScheduler.UseCases.Scheduling.Dto.Output;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Output;
using Mapster;

namespace DentalScheduler.UseCases.Scheduling.Mappings
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