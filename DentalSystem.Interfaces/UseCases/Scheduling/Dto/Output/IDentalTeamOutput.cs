using System;

namespace DentalSystem.Boundaries.UseCases.Scheduling.Dto.Output
{
    public interface IDentalTeamOutput
    {
        Guid? ReferenceId { get; set; }

        string Name { get; set; }

        IRoomOutput Room { get; }
    }
}