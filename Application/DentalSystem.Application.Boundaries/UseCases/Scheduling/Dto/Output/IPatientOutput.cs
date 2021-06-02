using System;

namespace DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output
{
    public interface IPatientOutput
    {
        Guid? ReferenceId { get; }

        byte[] Avatar { get; }

        string FirstName { get; }

        string LastName { get; }
    }
}