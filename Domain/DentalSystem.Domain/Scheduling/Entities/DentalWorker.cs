using System;
using DentalSystem.Domain.Identity.Entities;
using DentalSystem.Domain.Scheduling.Enumerations;
using DentalSystem.Domain.Common.Entities;

namespace DentalSystem.Domain.Scheduling.Entities
{
    public class DentalWorker : IEntity
    {
        public Guid Id { get; set; }

        public string IdentityUserId { get; set; }

        public virtual User IdentityUser { get; set; }

        public JobType JobType { get; set; }
    }
}