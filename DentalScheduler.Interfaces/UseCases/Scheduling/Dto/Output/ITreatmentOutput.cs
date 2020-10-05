using System;

namespace DentalScheduler.Interfaces.Dto.Output
{
    public interface ITreatmentOutput
    {
        Guid ReferenceId { get; set; }

        string Name { get; set; }

        int DurationInMinutes { get; set; }
    }
}