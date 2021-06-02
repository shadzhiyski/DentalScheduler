using System;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output;

namespace DentalSystem.Application.UseCases.Scheduling.Dto.Output
{
    public record TreatmentOutput : ITreatmentOutput
    {
        public Guid? ReferenceId { get; init; }

        public string Name { get; init; }

        public int DurationInMinutes { get; init; }
    }
}