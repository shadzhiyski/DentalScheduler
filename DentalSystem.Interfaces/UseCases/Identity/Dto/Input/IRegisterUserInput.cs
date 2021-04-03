namespace DentalSystem.Interfaces.UseCases.Identity.Dto.Input
{
    public interface IRegisterUserInput : IUserCredentialsInput
    {
        string FirstName { get; set; }

        string LastName { get; set; }
    }
}