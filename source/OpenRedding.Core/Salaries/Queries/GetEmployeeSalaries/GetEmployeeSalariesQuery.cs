namespace OpenRedding.Core.Salaries.Queries.GetEmployeeSalaries
{
    using System;
    using OpenRedding.Core.Infrastructure.Requests;
    using OpenRedding.Domain.Common.Aggregates;
    using OpenRedding.Domain.Salaries.Dtos;

    public class GetEmployeeSalariesQuery : OpenReddingRequest<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>
    {
        public GetEmployeeSalariesQuery(EmployeeSalarySearchRequestDto searchRequest, Uri gatewayBaseUrl, int? page) =>
            (SearchRequest, GatewayBaseUrl, Page) = (searchRequest, gatewayBaseUrl, page ?? 1);

        public EmployeeSalarySearchRequestDto SearchRequest { get; }

        public Uri GatewayBaseUrl { get; }

        public int Page { get; }
    }
}
