using Microsoft.AspNetCore.Identity;

namespace DentalSystem.Domain.Identity.Entities
{
    public class User : IdentityUser
    {
        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}