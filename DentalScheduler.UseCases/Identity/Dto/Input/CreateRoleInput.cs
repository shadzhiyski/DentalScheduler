using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;

namespace DentalScheduler.UseCases.Identity.Dto.Input
{
    public class CreateRoleInput : ICreateRoleInput
    {
        public string Name { get; set; }
    }
}