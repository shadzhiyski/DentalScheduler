using System;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;

namespace DentalSystem.Presentation.Web.UI.Scheduling.Models
{
    public class TreatmentSessionInputModel : ITreatmentSessionInput
    {
        public Guid ReferenceId { get; set; }

        public Guid? DentalTeamReferenceId { get; set; }

        public Guid? PatientReferenceId { get; set; }

        public Guid? TreatmentReferenceId { get; set; }

        public DateTimeOffset? Start { get; set; }

        public DateTimeOffset? End { get; set; }

        public string Status { get; set; }
    }
}