using DentalSystem.Application.UseCases.Scheduling.Dto.Output;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output;
using Mapster;

namespace DentalSystem.Application.UseCases.Scheduling.Mappings
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