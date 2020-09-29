using System;
using DentalScheduler.Interfaces.Dto.Output;

namespace DentalScheduler.Dto.Output
{
    public class RoomOutput : IRoomOutput
    {
        public Guid ReferenceId { get; set; }
        
        public string Name { get; set; }
    }
}