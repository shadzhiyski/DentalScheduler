namespace DentalScheduler.Interfaces.UseCases.Common.Dto.Output
{
    public interface IError
    {
        ErrorType Type { get; }
    }
}