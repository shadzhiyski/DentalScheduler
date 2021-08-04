using System;
using DentalSystem.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using DentalSystem.Domain.Scheduling.Enumerations;

namespace DentalSystem.Domain.Scheduling.Entities
{
    public class DentalWorker
    {
        public Guid Id { get; set; }

        public string IdentityUserId { get; set; }

        public virtual User IdentityUser { get; set; }

        public JobType JobType { get; set; }
    }
}