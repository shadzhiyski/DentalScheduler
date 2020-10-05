using DentalScheduler.Interfaces.Dto.Input;

namespace DentalScheduler.Dto.Input
{
    public class CreateRoleInput : ICreateRoleInput
    {
        public string Name { get; set; }
    }
}