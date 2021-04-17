using System;

namespace DentalSystem.Entities.Scheduling
{
    public class Treatment
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string Name { get; set; }

        public int DurationInMinutes { get; set; }
    }
}