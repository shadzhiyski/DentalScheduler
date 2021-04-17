using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;

namespace DentalSystem.Application.UseCases.Identity.Dto.Input
{
    public class RegisterUserInput : IRegisterUserInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}