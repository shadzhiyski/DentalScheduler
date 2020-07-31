using System;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.DAL.Models
{
    public class Dentist
    {
        public Guid Id { get; set; }

        public Guid IdentityUserId { get; set; }

        public virtual IdentityUser IdentityUser { get; set; }

        public JobType JobType { get; set; }
    }
}