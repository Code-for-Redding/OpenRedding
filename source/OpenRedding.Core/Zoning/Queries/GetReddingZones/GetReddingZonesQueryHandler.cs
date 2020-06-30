namespace OpenRedding.Core.Zoning.Queries.GetReddingZones
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Core.Data;
    using OpenRedding.Core.Extensions;
    using OpenRedding.Domain.Common.Aggregates;
    using OpenRedding.Domain.Zoning.Dtos;
    using OpenRedding.Shared;

    public class GetReddingZonesQueryHandler : IRequestHandler<GetReddingZonesQuery, OpenReddingSearchResultAggregate<ZoningSearchResultDto>>
    {
        private readonly IOpenReddingDbContext _context;
        private readonly ILogger<GetReddingZonesQueryHandler> _logger;

        public GetReddingZonesQueryHandler(IOpenReddingDbContext context, ILogger<GetReddingZonesQueryHandler> logger) =>
            (_context, _logger) = (context, logger);

        public async Task<OpenReddingSearchResultAggregate<ZoningSearchResultDto>> Handle(GetReddingZonesQuery request, CancellationToken cancellationToken)
        {
            ArgumentValidation.CheckNotNull(request, nameof(request));
            ArgumentValidation.CheckNotNull(request.SearchRequest, nameof(request.SearchRequest));

            // Get an enumerable collection of zones from the user's search request
            var queriedZones = _context.Zones
                .AsNoTracking()
                .FromSearchRequest(request.SearchRequest);

            // Get the total count to display on the aggregate and paginate results
            var totalResults = queriedZones.Count();
            var resultingZones = await queriedZones
                .Select(z => z.ToZoningSearchResultDto(request.ApiUrl))
                .SkipAndTakeDefault(request.Page)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new OpenReddingSearchResultAggregate<ZoningSearchResultDto>(resultingZones, totalResults, request.Page);
        }
    }
}
