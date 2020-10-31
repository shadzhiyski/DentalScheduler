using DentalScheduler.UseCases.Scheduling.Dto.Output;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Output;
using Mapster;

namespace DentalScheduler.UseCases.Scheduling.Config.Mappings
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