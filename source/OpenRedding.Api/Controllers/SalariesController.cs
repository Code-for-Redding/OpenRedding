namespace OpenRedding.Api.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using Domain.Salaries.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Net.Http.Headers;
    using OpenRedding.Api;
    using OpenRedding.Core.Exception;
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
        private readonly string _gatewayBaseUrl;

        public SalariesController(ILogger<SalariesController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _gatewayBaseUrl = configuration["GatewayBaseUrl"];
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

            var searchRequest = new EmployeeSalarySearchRequestDto(name, jobTitle, agency, status, sortBy, year, sortField, basePayRange, totalPayRange);

            var response = await Mediator.Send(new GetEmployeeSalariesQuery(searchRequest, new Uri(_gatewayBaseUrl), page));

            int nextPage;
            int previousPage;
            if (page.HasValue)
            {
                nextPage = page.Value < response.Pages ? page.Value + 1 : 1;
                previousPage = page.Value > 1 && page.Value <= response.Pages ? page.Value - 1 : response.Pages;
            }
            else
            {
                nextPage = response.Pages > 1 ? 2 : 1;
                previousPage = response.Pages;
            }

            var nextPageQuery = new QueryBuilder();
            var previousPageQuery = new QueryBuilder();
            var firstPageQuery = new QueryBuilder();
            var lastPageQuery = new QueryBuilder();
            var pagedLinkQuery = new QueryBuilder();
            var downloadLinkQuery = new QueryBuilder();

            foreach (var queryParam in HttpContext.Request.Query)
            {
                if (!string.Equals(queryParam.Key, "page", StringComparison.CurrentCultureIgnoreCase))
                {
                    nextPageQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    previousPageQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    firstPageQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    lastPageQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    pagedLinkQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    downloadLinkQuery.Add(queryParam.Key, queryParam.Value.ToString());
                }
            }

            nextPageQuery.Add("page", nextPage.ToString());
            previousPageQuery.Add("page", previousPage.ToString());
            firstPageQuery.Add("page", "1");
            lastPageQuery.Add("page", response.Pages.ToString());
            pagedLinkQuery.Add("page", OpenReddingConstants.PageNumberStringReplacementValue);

            var nextPageLink = $"{_gatewayBaseUrl}/salaries{nextPageQuery.ToQueryString()}";
            var previousPageLink = $"{_gatewayBaseUrl}/salaries{previousPageQuery.ToQueryString()}";
            var firstPageLink = $"{_gatewayBaseUrl}/salaries{firstPageQuery.ToQueryString()}";
            var lastPageLink = $"{_gatewayBaseUrl}/salaries{lastPageQuery.ToQueryString()}";
            var pagedPageLink = $"{_gatewayBaseUrl}/salaries{pagedLinkQuery.ToQueryString()}";
            var downloadRequestLink = $"{_gatewayBaseUrl}/salaries/download{downloadLinkQuery.ToQueryString()}";

            return new OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>
            {
                Results = response.Results,
                Count = response.Count,
                Pages = response.Pages,
                CurrentPage = response.CurrentPage,
                Links = new OpenReddingPagedLinks
                {
                    Next = new OpenReddingLink
                    {
                        Href = nextPageLink,
                        Rel = nameof(OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>),
                        Method = HttpMethod.Get.Method
                    },
                    Previous = new OpenReddingLink
                    {
                        Href = previousPageLink,
                        Rel = nameof(OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>),
                        Method = HttpMethod.Get.Method
                    },
                    First = new OpenReddingLink
                    {
                        Href = firstPageLink,
                        Rel = nameof(OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>),
                        Method = HttpMethod.Get.Method
                    },
                    Last = new OpenReddingLink
                    {
                        Href = lastPageLink,
                        Rel = nameof(OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>),
                        Method = HttpMethod.Get.Method
                    },
                    Paged = new OpenReddingLink
                    {
                        Href = pagedPageLink,
                        Rel = nameof(OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>),
                        Method = HttpMethod.Get.Method
                    },
                    Download = new OpenReddingLink
                    {
                        Href = downloadRequestLink,
                        Method = HttpMethod.Get.Method
                    }
                }
            };
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeSalaryDetailViewModel), StatusCodes.Status200OK)]
        public async Task<EmployeeSalaryDetailViewModel> GetEmployeeSalary([FromRoute] int id)
        {
            _logger.LogInformation($"Retrieving employee salary detail for employeeId [{id}]");
            return await Mediator.Send(new RetrieveEmployeeSalaryQuery(id, new Uri(_gatewayBaseUrl)));
        }

        [HttpPost("download")]
        public async Task<OpenReddingLink> DownloadSalaries([FromBody] EmployeeSalarySearchRequestDto? searchRequest)
        {
            _logger.LogInformation("Download salary CSV file...");
            return await Mediator.Send(new DownloadSalariesCommand(searchRequest ?? new EmployeeSalarySearchRequestDto()));
        }
    }
}
