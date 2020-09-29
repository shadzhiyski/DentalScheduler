using System;
using DentalScheduler.Interfaces.Dto.Output;

namespace DentalScheduler.Dto.Output
{
    public class TreatmentOutput : ITreatmentOutput
    {
        public Guid ReferenceId { get; set; }
        
        public string Name { get; set; }

        public int DurationInMinutes { get; set; }
    }
}