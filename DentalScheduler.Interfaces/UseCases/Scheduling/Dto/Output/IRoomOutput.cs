using System;

namespace DentalScheduler.Interfaces.Dto.Output
{
    public interface IRoomOutput
    {
        Guid ReferenceId { get; set; }

        string Name { get; set; }
    }
}