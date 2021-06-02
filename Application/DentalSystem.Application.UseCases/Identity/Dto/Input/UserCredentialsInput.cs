using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;

namespace DentalSystem.Application.UseCases.Identity.Dto.Input
{
    public record UserCredentialsInput : IUserCredentialsInput
    {
        public string UserName { get; init; }

        public string Password { get; init; }
    }
}