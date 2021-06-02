using System;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Output;

namespace DentalSystem.Application.UseCases.Scheduling.Dto.Output
{
    public record TreatmentSessionOutput : ITreatmentSessionOutput
    {
        public Guid? ReferenceId { get; init; }

        public Guid? PatientReferenceId { get; init; }

        IDentalTeamOutput ITreatmentSessionOutput.DentalTeam => DentalTeam;

        public DentalTeamOutput DentalTeam { get; init; }

        ITreatmentOutput ITreatmentSessionOutput.Treatment => Treatment;

        public TreatmentOutput Treatment { get; init; }

        IPatientOutput ITreatmentSessionOutput.Patient => Patient;

        public PatientOutput Patient { get; init; }

        public DateTimeOffset Start { get; init; }

        public DateTimeOffset End { get; init; }

        public string Status { get; init; }
    }
}