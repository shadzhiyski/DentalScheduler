using DentalSystem.Interfaces.UseCases.Identity.Dto.Input;

namespace DentalSystem.UseCases.Identity.Dto.Input
{
    public class RegisterUserInput : IRegisterUserInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}