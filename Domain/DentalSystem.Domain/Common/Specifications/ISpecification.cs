using System;
using System.Linq.Expressions;

namespace DentalSystem.Domain.Common.Specifications
{
    public interface ISpecification<T>
    {
        public Expression<Func<T , bool>> Condition { get; }

        public bool IsSatisfiedBy(T entity) => Condition.Compile().Invoke(entity);
    }
}