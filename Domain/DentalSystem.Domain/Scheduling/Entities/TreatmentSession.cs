using System;
using DentalSystem.Domain.Common.Entities;
using DentalSystem.Domain.Scheduling.Enumerations;

namespace DentalSystem.Domain.Scheduling.Entities
{
    public class TreatmentSession : IReferableEntity
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public Guid PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        public Guid DentalTeamId { get; set; }

        public virtual DentalTeam DentalTeam { get; set; }

        public Guid TreatmentId { get; set; }

        public virtual Treatment Treatment { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }

        public TreatmentSessionStatus Status { get; set; }
    }
}