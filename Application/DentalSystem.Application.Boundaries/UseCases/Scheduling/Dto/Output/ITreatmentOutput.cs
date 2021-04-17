using System;

namespace DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output
{
    public interface ITreatmentOutput
    {
        Guid? ReferenceId { get; set; }

        string Name { get; set; }

        int DurationInMinutes { get; set; }
    }
}