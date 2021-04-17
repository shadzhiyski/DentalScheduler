using System;
using DentalSystem.Boundaries.UseCases.Scheduling.Dto.Output;

namespace DentalSystem.UseCases.Scheduling.Dto.Output
{
    public class TreatmentOutput : ITreatmentOutput
    {
        public Guid? ReferenceId { get; set; }

        public string Name { get; set; }

        public int DurationInMinutes { get; set; }
    }
}