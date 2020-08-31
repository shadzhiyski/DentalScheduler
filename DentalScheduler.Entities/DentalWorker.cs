using System;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.Entities
{
    public class DentalWorker
    {
        public Guid Id { get; set; }

        public string IdentityUserId { get; set; }

        public virtual IdentityUser IdentityUser { get; set; }

        public JobType JobType { get; set; }
    }
}