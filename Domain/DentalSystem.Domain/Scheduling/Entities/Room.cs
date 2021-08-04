using System;

namespace DentalSystem.Domain.Scheduling.Entities
{
    public class Room
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string Name { get; set; }
    }
}