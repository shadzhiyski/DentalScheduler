using System;
using DentalSystem.Domain.Common.Entities;

namespace DentalSystem.Domain.Scheduling.Entities
{
    public class Treatment : IReferableEntity
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string Name { get; set; }

        public int DurationInMinutes { get; set; }
    }
}