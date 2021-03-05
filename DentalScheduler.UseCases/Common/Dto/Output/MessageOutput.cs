using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;

namespace DentalScheduler.UseCases.Common.Dto.Output
{
    public record MessageOutput : IMessageOutput
    {
        public MessageOutput(string message)
        {
            Message = message;
        }

        public string Message { get; init; }
    }
}