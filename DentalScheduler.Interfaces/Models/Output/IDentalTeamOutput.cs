using System;

namespace DentalScheduler.Interfaces.Models.Output
{
    public interface IDentalTeamOutput
    {
        Guid ReferenceId { get; set; }
        
        string Name { get; set; }

        IRoomOutput Room { get; set; }
    }
}