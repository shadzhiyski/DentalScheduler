using System;

namespace DentalSystem.Domain.Scheduling
{
    public class DentalTeam
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string Name { get; set; }

        public Guid RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}