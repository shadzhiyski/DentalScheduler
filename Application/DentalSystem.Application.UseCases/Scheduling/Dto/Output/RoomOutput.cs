using System;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output;

namespace DentalSystem.Application.UseCases.Scheduling.Dto.Output
{
    public class RoomOutput : IRoomOutput
    {
        public Guid? ReferenceId { get; set; }

        public string Name { get; set; }
    }
}