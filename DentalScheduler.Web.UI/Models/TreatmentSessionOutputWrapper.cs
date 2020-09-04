using System;
using DentalScheduler.DTO.Output;
using DentalScheduler.Interfaces.Models.Output;

namespace DentalScheduler.Web.UI.Models
{
    public class TreatmentSessionOutputWrapper
    {
        public TreatmentSessionOutputWrapper(ITreatmentSessionOutput treatmentSessionOutput)
        {
            TreatmentSessionOutput = treatmentSessionOutput;

        }

        public ITreatmentSessionOutput TreatmentSessionOutput { get; }

        public DateTime Start
        {
            get => TreatmentSessionOutput.Start.DateTime;
            set => TreatmentSessionOutput.Start = value;
        }

        public DateTime End
        {
            get => TreatmentSessionOutput.End.DateTime;
            set => TreatmentSessionOutput.End = value;
        }

        public string Reason
        {
            get => TreatmentSessionOutput.Reason;
            set => TreatmentSessionOutput.Reason = value;
        }
    }
}