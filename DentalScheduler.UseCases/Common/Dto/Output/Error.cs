using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.UseCases.Common.Dto.Output
{
    public class Error : IError, IMessageOutput
    {
        public Error(ErrorType type, string message)
        {
            Message = message;
            Type = type;
        }

        public ErrorType Type { get; }

        public string Message { get; set; }
    }
}