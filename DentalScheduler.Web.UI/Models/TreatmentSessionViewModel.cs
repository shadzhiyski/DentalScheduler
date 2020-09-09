using System;

namespace DentalScheduler.Web.UI.Models
{
    public class TreatmentSessionViewModel
    {
        public Guid PatientReferenceId { get; set; }

        public DentalTeamViewModel DentalTeam { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Reason { get; set; }
        
        public string Status { get; set; }
    }
}