namespace DentalScheduler.Interfaces.Models.Input
{
    public interface IRegisterUserInput : IUserCredentialsInput
    {
         RoleType RoleType { get; set; }
    }
}