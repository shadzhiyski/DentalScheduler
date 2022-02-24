using System;

namespace DentalSystem.Domain.Common.Entities
{
    public interface IReferableEntity : IEntity
    {
        public Guid ReferenceId { get; }
    }
}