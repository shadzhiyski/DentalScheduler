using System;
using DentalSystem.Boundaries.UseCases.Scheduling.Dto.Output;

namespace DentalSystem.UseCases.Scheduling.Dto.Output
{
    public class RoomOutput : IRoomOutput
    {
        public Guid? ReferenceId { get; set; }

        public string Name { get; set; }
    }
}