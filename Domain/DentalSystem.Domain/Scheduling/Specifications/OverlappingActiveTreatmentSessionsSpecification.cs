using System;
using System.Linq.Expressions;
using DentalSystem.Domain.Common.Specifications;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Domain.Scheduling.Enumerations;

namespace DentalSystem.Domain.Scheduling.Specifications
{
    public class OverlappingActiveTreatmentSessionsSpecification : ISpecification<TreatmentSession>
    {
        public Expression<Func<TreatmentSession, bool>> Condition { get; }

        public OverlappingActiveTreatmentSessionsSpecification(DateTimeOffset? start, DateTimeOffset? end)
        {
            Condition = (e) => e.Status != TreatmentSessionStatus.Rejected
                && e.Start <= end
                && e.End >= start;
        }
    }
}