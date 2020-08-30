using System;
using DentalScheduler.Interfaces.Models.Input;

namespace DentalScheduler.Web.UI.Models
{
    public class TreatmentSessionPeriodWrapperModel
    {
        public TreatmentSessionPeriodWrapperModel(ITreatmentSessionInput treatmentSessionInput)
        {
            TreatmentSessionInput = treatmentSessionInput;
        }

        public DateTime? Start 
        {
            get => TreatmentSessionInput.Start.Value.DateTime;
            set => TreatmentSessionInput.Start = value;
        }

        public DateTime? End 
        {
            get => TreatmentSessionInput.End.Value.DateTime;
            set => TreatmentSessionInput.End = value;
        }

        public ITreatmentSessionInput TreatmentSessionInput { get; }
    }
}