using System;
using DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Output;

namespace DentalScheduler.UseCases.Scheduling.Dto.Output
{
    public class TreatmentSessionOutput : ITreatmentSessionOutput
    {
        public Guid ReferenceId { get; set; }

        public Guid PatientReferenceId { get; set; }

        IDentalTeamOutput ITreatmentSessionOutput.DentalTeam => DentalTeam;

        public DentalTeamOutput DentalTeam { get; set; }

        ITreatmentOutput ITreatmentSessionOutput.Treatment => Treatment;

        public TreatmentOutput Treatment { get; set; }

        IPatientOutput ITreatmentSessionOutput.Patient => Patient;

        public PatientOutput Patient { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }

        public string Status { get; set; }
    }
}