using System;
using System.Linq.Expressions;
using DentalSystem.Domain.Common.Specifications;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Domain.Scheduling.Enumerations;

namespace DentalSystem.Domain.Scheduling.Specifications
{
    public class OverlappingTreatmentSessionsForPatientSpecification : ISpecification<TreatmentSession>
    {
        public Expression<Func<TreatmentSession, bool>> Condition { get; }

        public OverlappingTreatmentSessionsForPatientSpecification(
            Guid? patientReferenceId, DateTimeOffset? start, DateTimeOffset? end)
        {
            Condition = new GenericSpecification<TreatmentSession>(
                    (e) => e.Patient.ReferenceId == patientReferenceId
                )
                .And(new OverlappingActiveTreatmentSessionsSpecification(start, end))
                .Condition;
        }
    }
}