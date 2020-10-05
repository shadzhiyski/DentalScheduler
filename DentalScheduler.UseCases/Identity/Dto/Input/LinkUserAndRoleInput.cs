using DentalScheduler.Interfaces.Dto.Input;

namespace DentalScheduler.Dto.Input
{
    public class LinkUserAndRoleInput : ILinkUserAndRoleInput
    {
        public string UserName { get; set; }

        public string RoleName { get; set; }        
    }
}