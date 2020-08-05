using System;

namespace DentalScheduler.Entities
{
    public class TreatmentSessionDoctorCommitment
    {
        public Guid Id { get; set; }

        public Guid TreatmentSessionId { get; set; }

        public virtual TreatmentSession TreatmentSession { get; set; }

        public Guid DentalWorkerId { get; set; }

        public virtual DentalWorker DentalWorker { get; set; }
    }
}