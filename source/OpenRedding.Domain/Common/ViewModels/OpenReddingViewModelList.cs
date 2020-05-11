namespace OpenRedding.Domain.Common.ViewModels
{
    using Shared;

    public class OpenReddingViewModelList
    {
        public OpenReddingViewModelList(int count)
        {
            Count = count;
        }

        public int Pages => (Count / OpenReddingConstants.MaxPageSizeResult) + 1;

        public int Count { get; }
    }
}
