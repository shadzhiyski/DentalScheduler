using System;

namespace DentalScheduler.Interfaces.Models.Input
{
    public interface ITreatmentSessionInput
    {
        Guid? DentalTeamReferenceId { get; set; }

        Guid? PatientReferenceId { get; set; }

        Guid? TreatmentReferenceId { get; set; }

        DateTimeOffset? Start { get; set; }

        DateTimeOffset? End { get; set; }

        string Reason { get; set; }

        string Status { get; set; }
    }
}