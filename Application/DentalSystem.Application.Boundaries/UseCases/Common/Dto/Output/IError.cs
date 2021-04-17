namespace DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output
{
    public interface IError
    {
        ErrorType Type { get; }
    }
}