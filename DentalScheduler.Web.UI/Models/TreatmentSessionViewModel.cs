using System;

namespace DentalScheduler.Web.UI.Models
{
    public class TreatmentSessionViewModel
    {
        public Guid PatientReferenceId { get; set; }

        public DentalTeamViewModel DentalTeam { get; set; }

        public TreatmentViewModel Treatment { get; set; }

        public string TreatmentName => Treatment.Name;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
        
        public string Status { get; set; }
    }
}