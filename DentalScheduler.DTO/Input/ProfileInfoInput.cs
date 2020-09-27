using DentalScheduler.Interfaces.Models.Input;

namespace DentalScheduler.DTO.Input
{
    public class ProfileInfoInput : IProfileInfoInput
    {
        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}