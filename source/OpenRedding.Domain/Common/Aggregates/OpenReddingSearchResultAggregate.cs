namespace OpenRedding.Domain.Common.Aggregates
{
    using System.Collections.Generic;
    using OpenRedding.Shared;

    public class OpenReddingSearchResultAggregate<TResult>
    {
        public OpenReddingSearchResultAggregate(IEnumerable<TResult> results, int count, int currentPage) =>
            (Results, Count, CurrentPage) = (results, count, currentPage);

        public IEnumerable<TResult> Results { get; }

        public int Count { get; }

        public int Pages => (Count / OpenReddingConstants.MaxPageSizeResult) + 1;

        public int CurrentPage { get; }
    }
}
