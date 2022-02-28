using System;
using DentalSystem.Domain.Common.Entities;

namespace DentalSystem.Domain.Scheduling.Entities
{
    public class DentalTeam : IReferableEntity
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string Name { get; set; }

        public Guid RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}