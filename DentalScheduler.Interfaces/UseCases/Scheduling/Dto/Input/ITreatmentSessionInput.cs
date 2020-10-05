using System;

namespace DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Input
{
    public interface ITreatmentSessionInput
    {
        Guid ReferenceId { get; set; }

        Guid? DentalTeamReferenceId { get; set; }

        Guid? PatientReferenceId { get; set; }

        Guid? TreatmentReferenceId { get; set; }

        DateTimeOffset? Start { get; set; }

        DateTimeOffset? End { get; set; }

        string Status { get; set; }
    }
}