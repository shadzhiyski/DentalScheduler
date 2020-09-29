using System;
using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.DTO.Output
{
    public class TreatmentOutput : ITreatmentOutput
    {
        public Guid ReferenceId { get; set; }
        
        public string Name { get; set; }

        public int DurationInMinutes { get; set; }
    }
}