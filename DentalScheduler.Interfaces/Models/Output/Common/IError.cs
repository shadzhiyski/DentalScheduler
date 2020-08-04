namespace DentalScheduler.Interfaces.Models.Output.Common
{
    public interface IError : IMessageOutput
    {
        ErrorType Type { get; }
    }
}