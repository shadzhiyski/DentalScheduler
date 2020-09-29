namespace DentalScheduler.Interfaces.Dto.Input
{
    public interface IRegisterUserInput : IUserCredentialsInput
    {
         RoleType RoleType { get; set; }
    }
}