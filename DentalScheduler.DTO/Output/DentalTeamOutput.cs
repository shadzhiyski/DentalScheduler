using System;
using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.DTO.Output
{
    public class DentalTeamOutput : IDentalTeamOutput
    {
        public Guid ReferenceId { get; set; }
        
        public string Name { get; set; }

        public IRoomOutput Room { get; set; }
    }
}