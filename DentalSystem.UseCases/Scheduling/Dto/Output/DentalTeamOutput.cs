using System;
using DentalSystem.Interfaces.UseCases.Scheduling.Dto.Output;

namespace DentalSystem.UseCases.Scheduling.Dto.Output
{
    public class DentalTeamOutput : IDentalTeamOutput
    {
        public Guid ReferenceId { get; set; }

        public string Name { get; set; }

        IRoomOutput IDentalTeamOutput.Room { get; }

        public RoomOutput Room { get; set; }
    }
}