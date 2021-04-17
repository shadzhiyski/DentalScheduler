using System;

namespace DentalSystem.Entities.Scheduling
{
    public class Room
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string Name { get; set; }
    }
}