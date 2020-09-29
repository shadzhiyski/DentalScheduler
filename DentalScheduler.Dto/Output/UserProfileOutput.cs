using DentalScheduler.Interfaces.Dto.Output;

namespace DentalScheduler.Dto.Output
{
    public class UserProfileOutput : IUserProfileOutput
    {
        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}