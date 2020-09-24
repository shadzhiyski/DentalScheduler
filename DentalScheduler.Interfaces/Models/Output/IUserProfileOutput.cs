namespace DentalScheduler.Interfaces.Models.Output
{
    public interface IUserProfileOutput
    {
        byte[] Avatar { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }
    }
}