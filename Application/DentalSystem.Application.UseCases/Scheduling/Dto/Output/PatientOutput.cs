using System;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output;

namespace DentalSystem.Application.UseCases.Scheduling.Dto.Output
{
    public record PatientOutput : IPatientOutput
    {
        public Guid? ReferenceId { get; init; }

        public byte[] Avatar { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}