using System;
using DentalSystem.Application.Boundaries.UseCases.Scheduling.Dto.Input;
using DentalSystem.Presentation.Web.UI.Scheduling.Models;

namespace DentalSystem.Presentation.Web.UI.Models
{
    public class TreatmentSessionPeriodWrapperModel
    {
        public TreatmentSessionPeriodWrapperModel(TreatmentSessionInputModel treatmentSessionInput)
        {
            TreatmentSessionInput = treatmentSessionInput;
        }

        public DateTime? Start
        {
            get => TreatmentSessionInput.Start != null
                ? TreatmentSessionInput.Start.Value.DateTime
                : default(DateTime?);
            set => TreatmentSessionInput.Start = value;
        }

        public DateTime? End
        {
            get => TreatmentSessionInput.End != null
                ? TreatmentSessionInput.End.Value.DateTime
                : default(DateTime?);
            set => TreatmentSessionInput.End = value;
        }

        public TreatmentSessionInputModel TreatmentSessionInput { get; }
    }
}