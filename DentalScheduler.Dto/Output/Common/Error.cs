using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.Dto.Output.Common
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