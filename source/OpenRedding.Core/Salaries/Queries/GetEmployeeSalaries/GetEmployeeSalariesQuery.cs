namespace OpenRedding.Core.Salaries.Queries.GetEmployeeSalaries
{
    using System.Collections.Generic;
    using OpenRedding.Core.Infrastructure.Requests;
    using OpenRedding.Domain.Common.Aggregates;
    using OpenRedding.Domain.Salaries.Aggregates;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.ViewModels;

    public class GetEmployeeSalariesQuery : OpenReddingRequest<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>
    {
        public GetEmployeeSalariesQuery(EmployeeSalarySearchRequestDto searchRequest, int? page) =>
            (SearchRequest, Page) = (searchRequest, page ?? 1);

        public EmployeeSalarySearchRequestDto SearchRequest { get; }

        public int Page { get; }
    }
}
