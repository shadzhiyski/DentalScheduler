using System;
using DentalScheduler.Interfaces.Dto.Output;

namespace DentalScheduler.Dto.Output
{
    public class DentalTeamOutput : IDentalTeamOutput
    {
        public Guid ReferenceId { get; set; }
        
        public string Name { get; set; }

        IRoomOutput IDentalTeamOutput.Room { get; }

        public RoomOutput Room { get; set; }
    }
}