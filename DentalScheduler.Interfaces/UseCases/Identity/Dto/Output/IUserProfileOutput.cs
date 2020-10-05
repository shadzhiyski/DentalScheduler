namespace DentalScheduler.Interfaces.UseCases.Identity.Dto.Output
{
    public interface IUserProfileOutput
    {
        byte[] Avatar { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }
    }
}