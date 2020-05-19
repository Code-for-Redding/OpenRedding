namespace OpenRedding.Api.Controllers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Domain.Salaries.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Api;
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
            [FromQuery] int? range,
            [FromQuery] int? page,
            [FromQuery] string? sortField)
        {
            _logger.LogInformation($"Querying salaries: name [{name}], jobTitle [{jobTitle}], agency [{agency}], status [{status}], sortBy [{sortBy}]");
            var searchRequest = new EmployeeSalarySearchRequestDto(name, jobTitle, agency, status, sortBy, year, sortField, range);

            var response = await Mediator.Send(new GetEmployeeSalariesQuery(searchRequest, page));

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

            foreach (var queryParam in HttpContext.Request.Query)
            {
                if (!string.Equals(queryParam.Key, "page", StringComparison.CurrentCultureIgnoreCase))
                {
                    nextPageQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    previousPageQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    firstPageQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    lastPageQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    pagedLinkQuery.Add(queryParam.Key, queryParam.Value.ToString());
                }
            }

            nextPageQuery.Add("page", nextPage.ToString());
            previousPageQuery.Add("page", previousPage.ToString());
            firstPageQuery.Add("page", "1");
            lastPageQuery.Add("page", response.Pages.ToString());
            pagedLinkQuery.Add("page", OpenReddingConstants.PageNumberStringReplacementValue);

            var nextPageLink = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{nextPageQuery.ToQueryString()}";
            var previousPageLink = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{previousPageQuery.ToQueryString()}";
            var firstPageLink = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{firstPageQuery.ToQueryString()}";
            var lastPageLink = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{lastPageQuery.ToQueryString()}";
            var pagedPageLink = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{pagedLinkQuery.ToQueryString()}";

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
                        Method = HttpMethod.Get.Method,
                    },
                    Previous = new OpenReddingLink
                    {
                        Href = previousPageLink,
                        Rel = nameof(OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>),
                        Method = HttpMethod.Get.Method,
                    },
                    First = new OpenReddingLink
                    {
                        Href = firstPageLink,
                        Rel = nameof(OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>),
                        Method = HttpMethod.Get.Method,
                    },
                    Last = new OpenReddingLink
                    {
                        Href = lastPageLink,
                        Rel = nameof(OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>),
                        Method = HttpMethod.Get.Method,
                    },
                    Paged = new OpenReddingLink
                    {
                        Href = pagedPageLink,
                        Rel = nameof(OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>),
                        Method = HttpMethod.Get.Method,
                    }
                }
            };
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeSalaryDetailViewModel), StatusCodes.Status200OK)]
        public async Task<EmployeeSalaryDetailViewModel> GetEmployeeSalary([FromRoute] int id)
        {
            _logger.LogInformation($"Retrieving employee salary detail for employeeId [{id}]");
            return await Mediator.Send(new RetrieveEmployeeSalaryQuery(id));
        }
    }
}
