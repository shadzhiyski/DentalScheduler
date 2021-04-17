using System;
using DentalSystem.Boundaries.UseCases.Scheduling.Dto.Output;

namespace DentalSystem.Application.UseCases.Scheduling.Dto.Output
{
    public class DentalTeamOutput : IDentalTeamOutput
    {
        public Guid? ReferenceId { get; set; }

        public string Name { get; set; }

        IRoomOutput IDentalTeamOutput.Room => Room;

        public RoomOutput Room { get; set; }
    }
}