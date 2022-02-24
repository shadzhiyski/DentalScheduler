using System.Linq.Expressions;

namespace DentalSystem.Domain.Common.Specifications
{
    internal class ParameterReplacer : ExpressionVisitor
    {

        private readonly ParameterExpression parameter;

        protected override Expression VisitParameter(ParameterExpression node)
            => base.VisitParameter(this.parameter);

        internal ParameterReplacer(ParameterExpression parameter)
            => this.parameter = parameter;
    }
}