using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.DTO.Output.Common
{
    public class Error : IError
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