using System;
using DentalSystem.Domain.Common.Entities;

namespace DentalSystem.Domain.Common.Specifications
{
    public class EqualityByReferenceIdSpecification<T> : GenericSpecification<T>
        where T : IReferableEntity
    {
        public EqualityByReferenceIdSpecification(Guid? referenceId)
            : base((e) => e.ReferenceId == referenceId)
        { }
    }
}