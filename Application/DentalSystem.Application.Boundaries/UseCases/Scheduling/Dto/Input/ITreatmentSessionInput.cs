using System;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto;

namespace DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input
{
    public interface ITreatmentSessionInput : IPeriod
    {
        Guid ReferenceId { get; }

        Guid? DentalTeamReferenceId { get; }

        Guid? PatientReferenceId { get; }

        Guid? TreatmentReferenceId { get; }

        string Status { get; }
    }
}