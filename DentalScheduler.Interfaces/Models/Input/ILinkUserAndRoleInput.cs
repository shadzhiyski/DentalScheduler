namespace DentalScheduler.Interfaces.Models.Input
{
    public interface ILinkUserAndRoleInput
    {
        string UserName { get; set; }
        
        string RoleName { get; set; }
    }
}