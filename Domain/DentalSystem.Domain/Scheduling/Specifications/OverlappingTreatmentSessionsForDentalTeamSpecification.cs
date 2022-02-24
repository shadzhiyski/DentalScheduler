using System;
using System.Linq.Expressions;
using DentalSystem.Domain.Common.Specifications;
using DentalSystem.Domain.Scheduling.Entities;
using DentalSystem.Domain.Scheduling.Enumerations;

namespace DentalSystem.Domain.Scheduling.Specifications
{
    public class OverlappingTreatmentSessionsForDentalTeamSpecification : ISpecification<TreatmentSession>
    {
        public Expression<Func<TreatmentSession, bool>> Condition { get; }

        public OverlappingTreatmentSessionsForDentalTeamSpecification(
            Guid? dentalTeamReferenceId, DateTimeOffset? start, DateTimeOffset? end)
        {
            Condition = new GenericSpecification<TreatmentSession>(
                    (e) => e.DentalTeam.ReferenceId == dentalTeamReferenceId
                )
                .And(new OverlappingActiveTreatmentSessionsSpecification(start, end))
                .Condition;
        }
    }
}