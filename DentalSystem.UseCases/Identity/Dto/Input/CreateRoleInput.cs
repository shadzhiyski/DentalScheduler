using DentalSystem.Interfaces.UseCases.Identity.Dto.Input;

namespace DentalSystem.UseCases.Identity.Dto.Input
{
    public class CreateRoleInput : ICreateRoleInput
    {
        public string Name { get; set; }
    }
}