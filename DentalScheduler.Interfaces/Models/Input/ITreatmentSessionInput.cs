using System;

namespace DentalScheduler.Interfaces.Models.Input
{
    public interface ITreatmentSessionInput
    {
        Guid? DentalTeamId { get; set; }

        Guid? PatientId { get; set; }
        
        Guid? RoomId { get; set; }

        DateTimeOffset? Start { get; set; }

        DateTimeOffset? End { get; set; }

        string Reason { get; set; }
    }
}