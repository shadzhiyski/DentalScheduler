using System;

namespace DentalScheduler.DAL.Models
{
    public class TreatmentSession
    {
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        public Guid RoomId { get; set; }

        public virtual Room Room { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }

        public string Reason { get; set; }
    }
}