namespace OpenRedding.Domain.Common.Aggregates
{
    using System.Collections.Generic;
    using OpenRedding.Shared;

    public class OpenReddingSearchResultAggregate<TResult>
    {
        public OpenReddingSearchResultAggregate(IEnumerable<TResult> results, int count) =>
            (Results, Count) = (results, count);

        public IEnumerable<TResult> Results { get; }

        public int Count { get; }

        public int Pages => (Count / OpenReddingConstants.MaxPageSizeResult) + 1;
    }
}
