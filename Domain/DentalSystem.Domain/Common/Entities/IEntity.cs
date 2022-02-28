using System;

namespace DentalSystem.Domain.Common.Entities
{
    public interface IEntity
    {
        public Guid Id { get; }
    }
}