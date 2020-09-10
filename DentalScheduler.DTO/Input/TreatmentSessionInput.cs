using System;
using DentalScheduler.Interfaces.Models.Input;

namespace DentalScheduler.DTO.Input
{
    public class TreatmentSessionInput : ITreatmentSessionInput
    {
        public Guid? DentalTeamReferenceId { get; set; }

        public Guid? PatientReferenceId { get; set; }

        public Guid? TreatmentReferenceId { get; set; }

        public DateTimeOffset? Start { get; set; }

        public DateTimeOffset? End { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }
    }
}