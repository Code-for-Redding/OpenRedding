namespace OpenRedding.Infrastructure.Services
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Core.Data;
    using OpenRedding.Core.Exception;
    using OpenRedding.Core.Infrastructure.Services;
    using OpenRedding.Domain.Zoning.Dtos;
    using OpenRedding.Domain.Zoning.Entities;
    using OpenRedding.Infrastructure.Services.Seeds.Zoning;
    using OpenRedding.Shared;

    public class ZoningTableSeeder : IZoningTableSeeder
    {
        private readonly IOpenReddingDbContext _context;
        private readonly ILogger<ZoningTableSeeder> _logger;
        private readonly HttpClient _httpClient;

        public ZoningTableSeeder(IOpenReddingDbContext context, ILogger<ZoningTableSeeder> logger, HttpClient httpClient)
        {
            _context = context;
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task SeedZoningDataAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Pulling data from Transparent California...");

            // Call the endpoint to make sure we get the CSV data back
            _logger.LogInformation($"Calling CSV endpoint: {OpenReddingConstants.ZoningApiBaseUrl}...");
            var serializerOptions = new JsonSerializerOptions
            {
                Converters = { new ZoningClassEnumConverter() }
            };

            var zones = await _httpClient.GetFromJsonAsync<ArcGisZoningResponseDto>(OpenReddingConstants.ZoningApiBaseUrl, serializerOptions, cancellationToken).ConfigureAwait(false);

            if (zones is null || zones.Features is null)
            {
                throw new OpenReddingApiException("No response from ArcGIS API", HttpStatusCode.InternalServerError);
            }

            var zoneEntites = zones.Features.Select(f =>
            {
                ArgumentValidation.CheckNotNull(f.Properties, nameof(f.Properties));

                return new ReddingZone
                {
                    Zoning = f.Properties!.Zoning,
                    ZoningClass = f.Properties!.ZClass,
                    ZoningDescription = f.Properties!.ZDesc,
                    ZoningDescriptionExtended = f.Properties!.ZDesc2,
                    ZoneId = (int)f.Properties!.ZoneId,
                    BaseDist = f.Properties!.BaseDist,
                    OverlayDisctrict = f.Properties!.OverlayDist1,
                    OverlayDistrictExtended = f.Properties!.OverlayDist1,
                    ShapeArea = f.Properties!.ShapeArea,
                    ShapeLength = f.Properties!.ShapeLength,
                };
            }).ToList();

            // Add the collection and update the database
            _logger.LogInformation($"Seeding zoning data, {zoneEntites.Count} rows...");
            await _context.BulkInsertEntitiesAsync(zoneEntites, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            _logger.LogInformation("Data has been seeded successfully!");
        }
    }
}
