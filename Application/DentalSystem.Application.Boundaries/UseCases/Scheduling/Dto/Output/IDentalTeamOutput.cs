using System;

namespace DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output
{
    public interface IDentalTeamOutput
    {
        Guid? ReferenceId { get; }

        string Name { get; }

        IRoomOutput Room { get; }
    }
}