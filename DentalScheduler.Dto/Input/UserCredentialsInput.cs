using DentalScheduler.Interfaces.Models.Input;

namespace DentalScheduler.DTO.Input
{
    public class UserCredentialsInput : IUserCredentialsInput
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }
    }
}