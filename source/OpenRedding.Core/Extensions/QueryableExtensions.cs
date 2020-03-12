namespace OpenRedding.Core.Extensions
{
    using System;
    using System.Linq;
    using OpenRedding.Shared;

    public static class QueryableExtensions
    {
        public static IQueryable<T> SkipAndTakeDefault<T>(this IQueryable<T> queryable, int page)
            where T : class
        {
            if (queryable is null)
            {
                throw new ArgumentNullException(nameof(queryable), "Queryable cannot be null, please check LINQ statement");
            }

            return queryable
                .Skip(OpenReddingConstants.MaxPageSizeResult * (page - 1))
                .Take(OpenReddingConstants.MaxPageSizeResult);
        }
    }
}
