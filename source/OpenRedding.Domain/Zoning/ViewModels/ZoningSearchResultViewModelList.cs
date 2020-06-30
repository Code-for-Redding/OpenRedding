namespace OpenRedding.Domain.Zoning.ViewModels
{
    using System.Collections.Generic;
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Zoning.Dtos;

    public class ZoningSearchResultViewModelList : OpenReddingViewModelList
    {
        public ZoningSearchResultViewModelList(IEnumerable<ZoningSearchResultDto> zones, int count)
            : base(count)
        {
            Zones = zones;
        }

        public IEnumerable<ZoningSearchResultDto> Zones { get; }
    }
}
