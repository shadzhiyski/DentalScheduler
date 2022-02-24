using System;
using System.Linq.Expressions;

namespace DentalSystem.Domain.Common.Specifications
{
    public class GenericSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Condition { get; protected init; }

        public GenericSpecification(Expression<Func<T, bool>> condition)
        {
            Condition = condition;
        }
    }
}