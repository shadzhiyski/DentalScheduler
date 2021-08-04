using System;
using DentalSystem.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace DentalSystem.Domain.Scheduling
{
    public class Patient
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string IdentityUserId { get; set; }

        public virtual User IdentityUser { get; set; }
    }
}