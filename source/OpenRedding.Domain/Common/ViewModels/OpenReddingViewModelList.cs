namespace OpenRedding.Domain.Common.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Shared;

    public class OpenReddingViewModelList
    {
        public OpenReddingViewModelList(IEnumerable<object> resultSet) => Count = resultSet.Count();

        public int Count { get; }

        public int Pages => Count / OpenReddingConstants.MaxPageSizeResult;

        public int Previous { get; set; }

        public int Next { get; set; }
    }
}