using System;

namespace DentalSystem.Entities.Scheduling
{
    public class DentalTeamParticipant
    {
        public Guid Id { get; set; }

        public Guid TeamId { get; set; }

        public virtual DentalTeam Team { get; set; }

        public Guid ParticipantId { get; set; }

        public virtual DentalWorker Participant { get; set; }
    }
}