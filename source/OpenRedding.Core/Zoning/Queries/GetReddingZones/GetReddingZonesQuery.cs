namespace OpenRedding.Core.Zoning.Queries.GetReddingZones
{
    using System;
    using MediatR;
    using OpenRedding.Domain.Common.Aggregates;
    using OpenRedding.Domain.Zoning.Dtos;
    using OpenRedding.Domain.Zoning.ViewModels;

    public class GetReddingZonesQuery : IRequest<OpenReddingSearchResultAggregate<ZoningSearchResultDto>>
    {
        public GetReddingZonesQuery(ZoneSearchRequestDto searchRequest, Uri apiUrl, int? page) =>
            (SearchRequest, ApiUrl, Page) = (searchRequest, apiUrl, page ?? 1);

        public ZoneSearchRequestDto SearchRequest { get; }

        public Uri ApiUrl { get; }

        public int Page { get; }
    }
}
