using System;

namespace DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output
{
    public interface ITreatmentSessionOutput
    {
        Guid? ReferenceId { get; }

        Guid? PatientReferenceId { get; }

        IDentalTeamOutput DentalTeam { get; }

        ITreatmentOutput Treatment { get; }

        IPatientOutput Patient { get; }

        DateTimeOffset Start { get; }

        DateTimeOffset End { get; }

        string Status { get; }
    }
}