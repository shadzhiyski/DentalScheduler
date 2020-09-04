using System;

namespace DentalScheduler.DTO.Serialization.Json
{
    public class DeserializedDentalTeam
    {
        public Guid ReferenceId { get; set; }
        
        public string Name { get; set; }

        public DeserializedRoom Room { get; set; }
    }
}