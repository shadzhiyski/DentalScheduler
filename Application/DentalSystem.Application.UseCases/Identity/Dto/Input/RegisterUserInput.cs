using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;

namespace DentalSystem.Application.UseCases.Identity.Dto.Input
{
    public record RegisterUserInput : IRegisterUserInput
    {
        public string UserName { get; init; }

        public string Password { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}