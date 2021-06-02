using System;

namespace DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output
{
    public interface ITreatmentOutput
    {
        Guid? ReferenceId { get; }

        string Name { get; }

        int DurationInMinutes { get; }
    }
}