using DentalSystem.Interfaces.UseCases.Identity.Dto.Output;

namespace DentalSystem.UseCases.Identity.Dto.Output
{
    public class UserProfileOutput : IUserProfileOutput
    {
        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}