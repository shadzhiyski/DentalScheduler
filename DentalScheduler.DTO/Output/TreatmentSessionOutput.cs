using System;
using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.DTO.Output
{
    public class TreatmentSessionOutput : ITreatmentSessionOutput
    {
        public Guid PatientReferenceId { get; set; }

        public IDentalTeamOutput DentalTeam { get; set; }

        public ITreatmentOutput Treatment { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }

        public string Reason { get; set; }
        
        public string Status { get; set; }
    }
}