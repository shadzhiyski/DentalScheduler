using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input;

namespace DentalSystem.Application.UseCases.Identity.Dto.Input
{
    public class LinkUserAndRoleInput : ILinkUserAndRoleInput
    {
        public string UserName { get; set; }

        public string RoleName { get; set; }
    }
}