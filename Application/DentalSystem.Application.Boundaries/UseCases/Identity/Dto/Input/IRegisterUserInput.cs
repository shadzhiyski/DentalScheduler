namespace DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input
{
    public interface IRegisterUserInput : IUserCredentialsInput
    {
        string FirstName { get; }

        string LastName { get; }
    }
}