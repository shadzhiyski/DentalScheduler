using System;

namespace DentalSystem.Interfaces.UseCases.Scheduling.Dto.Output
{
    public interface ITreatmentSessionOutput
    {
        Guid ReferenceId { get; set; }

        Guid PatientReferenceId { get; set; }

        IDentalTeamOutput DentalTeam { get; }

        ITreatmentOutput Treatment { get; }

        IPatientOutput Patient { get; }

        DateTimeOffset Start { get; set; }

        DateTimeOffset End { get; set; }

        string Status { get; set; }
    }
}