using System;

namespace DentalScheduler.Web.UI.Models
{
    public class TreatmentViewModel
    {
        public Guid ReferenceId { get; set; }
        
        public string Name { get; set; }

        public int DurationInMinutes { get; set; }
    }
}