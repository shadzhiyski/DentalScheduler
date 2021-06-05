using System;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using MediatR;

namespace DentalSystem.Application.UseCases.Scheduling.Dto.Input
{
    public record TreatmentSessionInput : ITreatmentSessionInput, IRequest<IResult<IMessageOutput>>
    {
        public Guid? DentalTeamReferenceId { get; init; }

        public Guid? PatientReferenceId { get; init; }

        public Guid? TreatmentReferenceId { get; init; }

        public DateTimeOffset? Start { get; init; }

        public DateTimeOffset? End { get; init; }

        public string Status { get; init; }
    }
}