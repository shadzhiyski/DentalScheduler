namespace DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input
{
    public interface IUserCredentialsInput
    {
        string UserName { get; }

        string Password { get; }
    }
}