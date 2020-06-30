namespace OpenRedding.Api.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using OpenRedding.Core.Configuration;
    using OpenRedding.Core.Infrastructure.Services;
    using OpenRedding.Core.Zoning.Queries.GetReddingZones;
    using OpenRedding.Core.Zoning.Queries.GetZoningRegion;
    using OpenRedding.Domain.Common.Aggregates;
    using OpenRedding.Domain.Zoning.Dtos;
    using OpenRedding.Domain.Zoning.Enums;
    using OpenRedding.Shared;

    public class ZonesController : OpenReddingBaseController
    {
		private readonly ILogger<ZonesController> _logger;
		private readonly ILinkBuilder<ZoningSearchResultDto> _linkBuilder;
		private readonly Uri _apiBaseUrl;

        public ZonesController(ILogger<ZonesController> logger, ILinkBuilder<ZoningSearchResultDto> linkBuilder, IOptions<ApplicationSettings> settings)
        {
            ArgumentValidation.CheckNotNull(settings, nameof(settings));

            _logger = logger;
            _linkBuilder = linkBuilder;
            _apiBaseUrl = string.IsNullOrWhiteSpace(settings.Value.ApiBaseUrl) ?
                throw new ArgumentNullException(nameof(settings)) :
                new Uri(settings.Value.ApiBaseUrl);
        }

        [HttpGet("regions")]
        [ProducesResponseType(typeof(ZoningRegionsDto), StatusCodes.Status200OK)]
        public async Task<ZoningRegionsDto> GetZoningRegions()
        {
            _logger.LogInformation("Retrieving zoning regions...");
            return await Mediator.Send(new GetZoningRegionsQuery());
        }

        [HttpGet]
        public async Task<OpenReddingSearchResultAggregate<ZoningSearchResultDto>> GetZones(
            [FromQuery] string? zoning = null,
            [FromQuery] int? zoningClass = null,
            [FromQuery] int? page = null)
        {
            _logger.LogInformation($"Querying zones:\n" +
                $"zoning: [{zoning}]\n" +
                $"zoningClass: [{zoningClass}]\n" +
                $"page: [{page}]");
            return await Mediator.Send(new GetReddingZonesQuery(new ZoneSearchRequestDto(zoning, zoningClass), _apiBaseUrl, page));
        }
    }
}
