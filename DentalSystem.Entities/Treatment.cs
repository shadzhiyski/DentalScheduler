using System;

namespace DentalSystem.Entities
{
    public class Treatment
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string Name { get; set; }

        public int DurationInMinutes { get; set; }
    }
}