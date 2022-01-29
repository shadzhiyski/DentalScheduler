using System;

namespace DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input
{
    public interface ITreatmentSessionReferencesInput
    {
        Guid? DentalTeamReferenceId { get; }

        Guid? PatientReferenceId { get; }

        Guid? TreatmentReferenceId { get; }
    }
}