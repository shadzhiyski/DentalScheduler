using DentalScheduler.Interfaces.Dto.Output.Common;

namespace DentalScheduler.Dto.Output.Common
{
    public class MessageOutput : IMessageOutput
    {
        public MessageOutput(string message)
        {
            Message = message;
        }
        
        public string Message { get; set; }
    }
}