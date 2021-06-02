using System;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output;

namespace DentalSystem.Application.UseCases.Scheduling.Dto.Output
{
    public record DentalTeamOutput : IDentalTeamOutput
    {
        public Guid? ReferenceId { get; init; }

        public string Name { get; init; }

        IRoomOutput IDentalTeamOutput.Room => Room;

        public RoomOutput Room { get; init; }
    }
}