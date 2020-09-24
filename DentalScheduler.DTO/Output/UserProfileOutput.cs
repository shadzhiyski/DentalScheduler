using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.DTO.Output
{
    public class UserProfileOutput : IUserProfileOutput
    {
        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}