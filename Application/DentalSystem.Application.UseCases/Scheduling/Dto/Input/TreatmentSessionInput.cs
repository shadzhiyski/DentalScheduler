using System;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;

namespace DentalSystem.Application.UseCases.Scheduling.Dto.Input
{
    public record TreatmentSessionInput : ITreatmentSessionInput
    {
        public Guid ReferenceId { get; init; }

        public Guid? DentalTeamReferenceId { get; init; }

        public Guid? PatientReferenceId { get; init; }

        public Guid? TreatmentReferenceId { get; init; }

        public DateTimeOffset? Start { get; init; }

        public DateTimeOffset? End { get; init; }

        public string Status { get; init; }
    }
}