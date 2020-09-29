using System;
using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.DTO.Output
{
    public class RoomOutput : IRoomOutput
    {
        public Guid ReferenceId { get; set; }
        
        public string Name { get; set; }
    }
}