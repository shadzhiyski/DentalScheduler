using DentalSystem.Application.UseCases.Scheduling.Dto.Output;
using DentalSystem.Entities.Scheduling;
using DentalSystem.Boundaries.UseCases.Scheduling.Dto.Output;
using Mapster;

namespace DentalSystem.Application.UseCases.Scheduling.Mappings
{
    public class DentalTeamMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<DentalTeam, IDentalTeamOutput>()
                .MapWith((src) =>
                new DentalTeamOutput
                {
                    Name = src.Name,
                    ReferenceId = src.ReferenceId,
                    Room = src.Room.Adapt<RoomOutput>(config)
                });
        }
    }
}