using DentalScheduler.Interfaces.Models.Input;

namespace DentalScheduler.DTO.Input
{
    public class CreateRoleInput : ICreateRoleInput
    {
        public string Name { get; set; }
    }
}