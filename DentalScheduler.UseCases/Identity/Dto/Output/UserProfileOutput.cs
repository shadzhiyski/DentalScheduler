using DentalScheduler.Interfaces.UseCases.Identity.Dto.Output;

namespace DentalScheduler.UseCases.Identity.Dto.Output
{
    public class UserProfileOutput : IUserProfileOutput
    {
        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}