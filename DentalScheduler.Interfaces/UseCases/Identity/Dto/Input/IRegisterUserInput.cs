namespace DentalScheduler.Interfaces.UseCases.Identity.Dto.Input
{
    public interface IRegisterUserInput : IUserCredentialsInput
    {
         RoleType RoleType { get; set; }
    }
}