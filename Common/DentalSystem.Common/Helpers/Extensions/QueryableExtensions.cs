using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DentalSystem.Common.Helpers.Extensions
{
    public static class QueryableExtensions
    {
        public static bool None<TSource>(this IQueryable<TSource> source) => source.Any();

        public static bool None<TSource>(
            this IQueryable<TSource> source,
            Func<TSource, bool> predicate)
            => source.Any(predicate);

        public static async Task<bool> NoneAsync<TSource>(this IQueryable<TSource> source)
            => await Task.Run(() => !(source.Any()));

        public static async Task<bool> NoneAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken)
            => await Task.Run(() => !(source.Any(predicate)));
    }
}