using System;
using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.DTO.Output
{
    public class TreatmentSessionOutput : ITreatmentSessionOutput
    {
        public Guid PatientReferenceId { get; set; }

        IDentalTeamOutput ITreatmentSessionOutput.DentalTeam => DentalTeam;

        public DentalTeamOutput DentalTeam { get; set; }

        ITreatmentOutput ITreatmentSessionOutput.Treatment => Treatment;

        public TreatmentOutput Treatment { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }
    }
}