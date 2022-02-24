using System;
using DentalSystem.Domain.Common.Entities;

namespace DentalSystem.Domain.Common.Specifications
{
    public class DifferenceByReferenceIdSpecification<T> : GenericSpecification<T>
        where T : IReferableEntity
    {
        public DifferenceByReferenceIdSpecification(Guid? referenceId)
            : base((e) => e.ReferenceId != referenceId)
        { }
    }
}