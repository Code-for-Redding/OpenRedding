namespace OpenRedding.Domain.Common.ViewModels
{
    using System;
    using Shared;

    public class OpenReddingViewModelList
    {
        public OpenReddingViewModelList(int count)
        {
            Count = count;
        }

        public int Pages => Count / OpenReddingConstants.MaxPageSizeResult;

        public int Count { get; }

        /*
        public Uri Next { get; }

        public Uri Previous { get; }

        public Uri First { get; }

        public Uri Last { get; }
         */
    }
}
