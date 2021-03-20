using DentalSystem.Interfaces.UseCases.Identity.Dto.Input;

namespace DentalSystem.UseCases.Identity.Dto.Input
{
    public class LinkUserAndRoleInput : ILinkUserAndRoleInput
    {
        public string UserName { get; set; }

        public string RoleName { get; set; }
    }
}