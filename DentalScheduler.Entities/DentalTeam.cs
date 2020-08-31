using System;

namespace DentalScheduler.Entities
{
    public class DentalTeam
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string Name { get; set; }
    }
}