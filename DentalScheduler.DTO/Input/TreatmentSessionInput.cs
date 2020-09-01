using System;
using DentalScheduler.Interfaces.Models.Input;

namespace DentalScheduler.DTO.Input
{
    public class TreatmentSessionInput : ITreatmentSessionInput
    {
        public Guid? DentalTeamId { get; set; }

        public Guid? PatientId { get; set; }

        public DateTimeOffset? Start { get; set; }

        public DateTimeOffset? End { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }

    }
}