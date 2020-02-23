namespace OpenRedding.Domain.Common.ViewModels
{
    using Shared;

    public class OpenReddingViewModelList
    {
        public OpenReddingViewModelList(int totalResults) =>
            Count = totalResults;

        public int Count { get; }

        public int Pages => Count / OpenReddingConstants.MaxPageSizeResult;
    }
}