using DentalSystem.Interfaces.UseCases.Identity.Dto.Input;

namespace DentalSystem.UseCases.Identity.Dto.Input
{
    public class UserCredentialsInput : IUserCredentialsInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}