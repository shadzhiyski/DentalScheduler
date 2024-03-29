using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DentalSystem.Common.Helpers.Extensions
{
    public static class QueryableExtensions
    {
        public static bool None<TSource>(this IQueryable<TSource> source) => !source.Any();

        public static bool None<TSource>(
            this IQueryable<TSource> source,
            Func<TSource, bool> predicate)
            => !source.Any(predicate);

        public static async Task<bool> NoneAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken)
            => !(await source.AnyAsync(cancellationToken));

        public static async Task<bool> NoneAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken)
            => !(await source.AnyAsync(predicate, cancellationToken));
    }
}