using System;

namespace DentalScheduler.Entities
{
    public class TreatmentSessionDoctorCommitment
    {
        public Guid Id { get; set; }

        public Guid TreatmentSessionId { get; set; }

        public virtual TreatmentSession TreatmentSession { get; set; }

        public Guid DentistId { get; set; }

        public virtual Dentist Dentist { get; set; }
    }
}