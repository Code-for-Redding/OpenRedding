namespace OpenRedding.Core.Zoning.Queries.GetZoningRegion
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OpenRedding.Core.Data;
    using OpenRedding.Domain.Zoning.Dtos;
    using OpenRedding.Shared;

    public class GetZoningRegionQueryHandler : IRequestHandler<GetZoningRegionsQuery, ZoningRegionsDto>
    {
        private readonly IOpenReddingDbContext _context;

        public GetZoningRegionQueryHandler(IOpenReddingDbContext context) =>
            _context = context;

        public async Task<ZoningRegionsDto> Handle(GetZoningRegionsQuery request, CancellationToken cancellationToken)
        {
            ArgumentValidation.CheckNotNull(request, nameof(request));

            // Gather a list of zoning regions from the database
            // These are subject to change frequently, hence why there is no enumeration
            var zoningRegions = await _context.Zones
                .AsNoTracking()
                .Where(z => !string.IsNullOrWhiteSpace(z.Zoning))
                .Select(z => z.Zoning!)
                .Distinct()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            // Add the "All Zones" selection
            zoningRegions.Insert(0, "All Zones");

            return new ZoningRegionsDto(zoningRegions);
        }
    }
}
