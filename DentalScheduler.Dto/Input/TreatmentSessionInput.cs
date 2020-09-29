using System;
using DentalScheduler.Interfaces.Models.Input;

namespace DentalScheduler.Dto.Input
{
    public class TreatmentSessionInput : ITreatmentSessionInput
    {
        public Guid ReferenceId { get; set; }
        
        public Guid? DentalTeamReferenceId { get; set; }

        public Guid? PatientReferenceId { get; set; }

        public Guid? TreatmentReferenceId { get; set; }

        public DateTimeOffset? Start { get; set; }

        public DateTimeOffset? End { get; set; }

        public string Status { get; set; }
    }
}