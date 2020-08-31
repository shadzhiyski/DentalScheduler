using System;

namespace DentalScheduler.Interfaces.Models.Output
{
    public interface IRoomOutput
    {
        Guid ReferenceId { get; set; }

        string Name { get; set; }
    }
}