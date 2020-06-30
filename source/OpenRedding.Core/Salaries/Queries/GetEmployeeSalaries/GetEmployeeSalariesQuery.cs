namespace OpenRedding.Core.Salaries.Queries.GetEmployeeSalaries
{
    using System;
    using OpenRedding.Core.Infrastructure.Requests;
    using OpenRedding.Domain.Common.Aggregates;
    using OpenRedding.Domain.Salaries.Dtos;

    public class GetEmployeeSalariesQuery : OpenReddingRequest<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>
    {
        public GetEmployeeSalariesQuery(EmployeeSalarySearchRequestDto searchRequest, Uri apiBaseUrl, int? page) =>
            (SearchRequest, ApiBaseUrl, Page) = (searchRequest, apiBaseUrl, page ?? 1);

        public EmployeeSalarySearchRequestDto SearchRequest { get; }

        public Uri ApiBaseUrl { get; }

        public int Page { get; }
    }
}
