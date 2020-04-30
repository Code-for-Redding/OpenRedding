namespace OpenRedding.Core.Salaries.Queries.GetEmployeeSalaries
{
    using OpenRedding.Core.Infrastructure.Requests;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.ViewModels;

    public class GetEmployeeSalariesQuery : OpenReddingRequest<EmployeeSearchResultList>
    {
        public GetEmployeeSalariesQuery(EmployeeSalarySearchRequestDto searchRequest, int page = 1) =>
            (SearchRequest, Page) = (searchRequest, page);

        public EmployeeSalarySearchRequestDto SearchRequest { get; }

        public int Page { get; }
    }
}
