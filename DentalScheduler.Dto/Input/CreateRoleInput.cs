using DentalScheduler.Interfaces.Models.Input;

namespace DentalScheduler.Dto.Input
{
    public class CreateRoleInput : ICreateRoleInput
    {
        public string Name { get; set; }
    }
}