namespace OpenRedding.Domain.Common.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using OpenRedding.Domain.Common.Aggregates;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Shared;

    public class OpenReddingPagedViewModel<TPagedResult>
    {
        public OpenReddingPagedViewModel(OpenReddingSearchResultAggregate<TPagedResult> aggregate, OpenReddingPagedLinks links) =>
            (Results, Count, Pages, Links) = (aggregate.Results, aggregate.Count, aggregate.Pages, links);

        public int Count { get; }

        public int Pages { get; }

        public IEnumerable<TPagedResult> Results { get; }

        public OpenReddingPagedLinks Links { get; }
    }
}
