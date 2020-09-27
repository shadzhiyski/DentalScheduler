namespace DentalScheduler.Interfaces.Models.Input
{
    public interface IProfileInfoInput
    {
        byte[] Avatar { get; set; }
  
        string FirstName { get; set; }

        string LastName { get; set; }
    }
}