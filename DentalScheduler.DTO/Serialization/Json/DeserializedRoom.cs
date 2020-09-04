using System;

namespace DentalScheduler.DTO.Serialization.Json
{
    public class DeserializedRoom
    {
        public Guid ReferenceId { get; set; }
        
        public string Name { get; set; }
    }
}