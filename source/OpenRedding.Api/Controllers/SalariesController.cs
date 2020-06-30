namespace OpenRedding.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Salaries.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using OpenRedding.Api;
    using OpenRedding.Core.Configuration;
    using OpenRedding.Core.Infrastructure.Services;
    using OpenRedding.Core.Salaries.Commands.DownloadSalaries;
    using OpenRedding.Core.Salaries.Queries.GetEmployeeSalaries;
    using OpenRedding.Core.Salaries.Queries.RetrieveEmployeeSalary;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Shared;

    public class SalariesController : OpenReddingBaseController
    {
        private readonly ILogger<SalariesController> _logger;
        private readonly Uri _apiBaseUrl;
        private readonly ILinkBuilder<EmployeeSalarySearchResultDto> _linkBuilder;

        public SalariesController(ILogger<SalariesController> logger, IOptions<ApplicationSettings> settings, ILinkBuilder<EmployeeSalarySearchResultDto> linkBuilder)
        {
            ArgumentValidation.CheckNotNull(settings, nameof(settings));

            _logger = logger;
            _linkBuilder = linkBuilder;
            _apiBaseUrl = string.IsNullOrWhiteSpace(settings.Value.ApiBaseUrl) ?
                throw new ArgumentNullException(nameof(settings)) :
                new Uri(settings.Value.ApiBaseUrl);
        }

        [HttpGet]
        [ProducesResponseType(typeof(OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>), StatusCodes.Status200OK)]
        public async Task<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>> GetEmployeeSalaries(
            [FromQuery] string? name,
            [FromQuery] string? jobTitle,
            [FromQuery] string? agency,
            [FromQuery] string? status,
            [FromQuery] string? sortBy,
            [FromQuery] int? year,
            [FromQuery] int? basePayRange,
            [FromQuery] int? totalPayRange,
            [FromQuery] int? page,
            [FromQuery] string? sortField)
        {
            _logger.LogInformation($"Querying salaries:\n" +
                $"name [{name}]\n" +
                $"jobTitle [{jobTitle}]\n" +
                $"agency [{agency}]\n" +
                $"status [{status}]\n" +
                $"sortBy [{sortBy}]\n" +
                $"year [{year}]\n" +
                $"basePayRange [{basePayRange}]\n" +
                $"totalPayRange [{totalPayRange}]\n" +
                $"page [{page}]\n" +
                $"sortField [{sortField}]");

            _logger.LogInformation("Sending employee salary search request...");
            var searchRequest = new EmployeeSalarySearchRequestDto(name, jobTitle, agency, status, sortBy, year, sortField, basePayRange, totalPayRange);

            var response = await Mediator.Send(new GetEmployeeSalariesQuery(searchRequest, _apiBaseUrl, page));
            _logger.LogInformation($"Request was successful, {response.Results.Count()} found with {response.Count} results");

            return new OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>
            {
                Results = response.Results,
                Count = response.Count,
                Pages = response.Pages,
                CurrentPage = response.CurrentPage,
                Links = _linkBuilder.BuildPaginationLinks(HttpContext.Request.Query, response.Pages, page)
            };
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeSalaryDetailViewModel), StatusCodes.Status200OK)]
        public async Task<EmployeeSalaryDetailViewModel> GetEmployeeSalary([FromRoute] int id)
        {
            _logger.LogInformation($"Retrieving employee salary detail for employeeId [{id}]");
            return await Mediator.Send(new RetrieveEmployeeSalaryQuery(id, _apiBaseUrl));
        }

        [HttpPost("download")]
        public async Task<OpenReddingLink> DownloadSalaries([FromBody] EmployeeSalarySearchRequestDto? searchRequest)
        {
            _logger.LogInformation($"Download salary CSV file...");
            return await Mediator.Send(new DownloadSalariesCommand(searchRequest ?? new EmployeeSalarySearchRequestDto()));
        }
    }
}
