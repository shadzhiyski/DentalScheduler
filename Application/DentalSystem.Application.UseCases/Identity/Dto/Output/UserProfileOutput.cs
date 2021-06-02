using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Output;

namespace DentalSystem.Application.UseCases.Identity.Dto.Output
{
    public record UserProfileOutput : IUserProfileOutput
    {
        public byte[] Avatar { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}