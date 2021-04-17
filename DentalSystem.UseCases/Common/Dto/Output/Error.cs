using DentalSystem.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.UseCases.Common.Dto.Output
{
    public record Error : IError, IMessageOutput
    {
        public Error(ErrorType type, string message)
        {
            Message = message;
            Type = type;
        }

        public ErrorType Type { get; init; }

        public string Message { get; init; }
    }
}