namespace DentalScheduler.Interfaces.Models.Output.Common
{
    public interface IError
    {
        ErrorType Type { get; }
    }
}