using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.Entities.Identity
{
    public class User : IdentityUser
    {
        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}