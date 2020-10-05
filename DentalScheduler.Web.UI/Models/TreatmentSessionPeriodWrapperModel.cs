using System;
using DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Input;

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

        public ITreatmentSessionInput TreatmentSessionInput { get; }
    }
}