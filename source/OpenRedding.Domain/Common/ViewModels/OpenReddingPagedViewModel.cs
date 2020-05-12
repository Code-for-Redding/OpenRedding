namespace OpenRedding.Domain.Common.ViewModels
{
    using System.Collections.Generic;
    using OpenRedding.Domain.Common.Miscellaneous;

    public class OpenReddingPagedViewModel<TPagedResult>
    {
        public int Count { get; set; }

        public int Pages { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<TPagedResult>? Results { get; set; }

        public OpenReddingPagedLinks? Links { get; set; }
    }
}
