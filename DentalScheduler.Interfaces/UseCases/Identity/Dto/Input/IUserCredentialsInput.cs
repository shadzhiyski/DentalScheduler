namespace DentalScheduler.Interfaces.Dto.Input
{
    public interface IUserCredentialsInput
    {
        string UserName { get; set; }

        string Password { get; set; }
    }
}