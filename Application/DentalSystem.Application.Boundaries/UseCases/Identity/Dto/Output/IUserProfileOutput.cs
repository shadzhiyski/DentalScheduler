namespace DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Output
{
    public interface IUserProfileOutput
    {
        byte[] Avatar { get; }

        string FirstName { get; }

        string LastName { get; }
    }
}