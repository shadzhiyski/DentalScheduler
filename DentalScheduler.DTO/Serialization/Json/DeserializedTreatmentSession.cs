using System;

namespace DentalScheduler.DTO.Serialization.Json
{
    public class DeserializedTreatmentSession
    {
        public Guid PatientReferenceId { get; set; }

        public DeserializedDentalTeam DentalTeam { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }

        public string Reason { get; set; }
        
        public string Status { get; set; }
    }
}