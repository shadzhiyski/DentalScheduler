using System;
using System.Linq.Expressions;

namespace DentalSystem.Domain.Common.Specifications
{
    public class AndSpecification<T> : GenericSpecification<T>
    {
        public AndSpecification(Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression)
            : base(ApplyAndOperation(leftExpression, rightExpression))
        { }

        private static Expression<Func<T, bool>> ApplyAndOperation(
            Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression)
        {
            Expression body = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            var parameter = Expression.Parameter(typeof(T));
            body = (BinaryExpression)new ParameterReplacer(parameter).Visit(body);

            body = body ?? throw new InvalidOperationException("Binary expression cannot be null.");

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}