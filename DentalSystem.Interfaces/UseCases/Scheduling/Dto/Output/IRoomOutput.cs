using System;

namespace DentalSystem.Interfaces.UseCases.Scheduling.Dto.Output
{
    public interface IRoomOutput
    {
        Guid ReferenceId { get; set; }

        string Name { get; set; }
    }
}