using System;
using DentalScheduler.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.Entities
{
    public class DentalWorker
    {
        public Guid Id { get; set; }

        public string IdentityUserId { get; set; }

        public virtual User IdentityUser { get; set; }

        public JobType JobType { get; set; }
    }
}