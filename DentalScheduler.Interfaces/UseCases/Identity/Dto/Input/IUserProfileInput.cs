using System.IO;

namespace DentalScheduler.Interfaces.UseCases.Identity.Dto.Input
{
    public interface IUserProfileInput
    {
        byte[] Avatar { get; }

        string FirstName { get; set; }

        string LastName { get; set; }
    }
}