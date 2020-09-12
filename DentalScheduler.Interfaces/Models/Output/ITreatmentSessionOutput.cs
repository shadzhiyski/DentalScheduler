using System;

namespace DentalScheduler.Interfaces.Models.Output
{
    public interface ITreatmentSessionOutput
    {
        Guid PatientReferenceId { get; set; }

        IDentalTeamOutput DentalTeam { get; }

        ITreatmentOutput Treatment { get; }

        DateTimeOffset Start { get; set; }

        DateTimeOffset End { get; set; }

        string Reason { get; set; }

        string Status { get; set; }
    }
}