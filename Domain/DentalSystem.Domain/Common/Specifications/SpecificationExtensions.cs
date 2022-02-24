namespace DentalSystem.Domain.Common.Specifications
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> And<T>(this ISpecification<T> specification, ISpecification<T> andSpecification)
            => new AndSpecification<T>(specification.Condition, andSpecification.Condition);

        public static ISpecification<T> Or<T>(this ISpecification<T> specification, ISpecification<T> orSpecification)
            => new OrSpecification<T>(specification.Condition, orSpecification.Condition);
    }
}