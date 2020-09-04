using DentalScheduler.DTO.Output;
using DentalScheduler.DTO.Serialization.Json;
using DentalScheduler.Entities;
using DentalScheduler.Interfaces.Models.Output;
using Mapster;

namespace DentalScheduler.Config.Mappings
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
                    Room = src.Room.Adapt<IRoomOutput>(config)
                });
            
            config.NewConfig<DeserializedDentalTeam, IDentalTeamOutput>()
                .MapWith((src) => 
                new DentalTeamOutput
                {
                    Name = src.Name,
                    ReferenceId = src.ReferenceId,
                    Room = src.Room.Adapt<IRoomOutput>(config)
                });
        }
    }
}