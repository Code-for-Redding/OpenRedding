namespace OpenRedding.Core.Salaries.Queries.GetEmployeeSalaries
{
    using OpenRedding.Core.Infrastructure.Requests;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.ViewModels;

    public class GetEmployeeSalariesQuery : OpenReddingRequest<EmployeeSearchResultViewModelList>
    {
        public GetEmployeeSalariesQuery(EmployeeSalarySearchRequestDto searchRequest, int? page) =>
            (SearchRequest, Page) = (searchRequest, page ?? 1);

        public EmployeeSalarySearchRequestDto SearchRequest { get; }

        public int Page { get; }
    }
}
