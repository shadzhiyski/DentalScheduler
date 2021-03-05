using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;

namespace DentalScheduler.UseCases.Identity.Dto.Input
{
    public class RegisterUserInput : IRegisterUserInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public RoleType RoleType { get; set; } = RoleType.Patient;
    }
}