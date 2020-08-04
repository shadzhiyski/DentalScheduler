namespace DentalScheduler.Interfaces.Models.Input
{
    public interface IUserCredentialsInput
    {
        string UserName { get; set; }

        string Password { get; set; }
    }
}