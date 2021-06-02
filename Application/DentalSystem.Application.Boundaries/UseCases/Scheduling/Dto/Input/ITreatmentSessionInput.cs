using System;

namespace DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input
{
    public interface ITreatmentSessionInput
    {
        Guid ReferenceId { get; }

        Guid? DentalTeamReferenceId { get; }

        Guid? PatientReferenceId { get; }

        Guid? TreatmentReferenceId { get; }

        DateTimeOffset? Start { get; }

        DateTimeOffset? End { get; }

        string Status { get; }
    }
}