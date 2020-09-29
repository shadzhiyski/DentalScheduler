using DentalScheduler.Interfaces.Models.Input;

namespace DentalScheduler.Dto.Input
{
    public class RegisterUserInput : IRegisterUserInput
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }

        public RoleType RoleType { get; set; } = RoleType.Patient;
    }
}