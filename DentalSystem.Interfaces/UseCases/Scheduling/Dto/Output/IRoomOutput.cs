using System;

namespace DentalSystem.Boundaries.UseCases.Scheduling.Dto.Output
{
    public interface IRoomOutput
    {
        Guid? ReferenceId { get; set; }

        string Name { get; set; }
    }
}