using System;
using DentalSystem.Domain.Common.Entities;
using DentalSystem.Domain.Identity.Entities;

namespace DentalSystem.Domain.Scheduling.Entities
{
    public class Patient : IReferableEntity
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string IdentityUserId { get; set; }

        public virtual User IdentityUser { get; set; }
    }
}