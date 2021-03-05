using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;

namespace DentalScheduler.UseCases.Identity.Dto.Input
{
    public class LinkUserAndRoleInput : ILinkUserAndRoleInput
    {
        public string UserName { get; set; }

        public string RoleName { get; set; }
    }
}