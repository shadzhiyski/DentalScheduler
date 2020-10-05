using System;

namespace DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Output
{
    public interface IDentalTeamOutput
    {
        Guid ReferenceId { get; set; }
        
        string Name { get; set; }

        IRoomOutput Room { get; }
    }
}