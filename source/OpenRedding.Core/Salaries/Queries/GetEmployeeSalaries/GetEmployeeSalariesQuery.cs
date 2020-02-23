namespace OpenRedding.Core.Salaries.Queries.GetEmployeeSalaries
{
    using Common;
    using Domain.Salaries.ViewModels;

    public class GetEmployeeSalariesQuery : OpenReddingRequest<EmployeeSearchResultList>
    {
        public GetEmployeeSalariesQuery(string? name, string? jobTitle, string? agency, string? status, string? sortBy, int page = 1) =>
            (Name, JobTitle, Agency, Status, SortBy, Page) = (name, jobTitle, agency, status, sortBy, page);

        public string? Name { get; }

        public string? JobTitle { get; }

        public string? Agency { get; }

        public string? Status { get; }

        public string? SortBy { get; }

        public int Page { get; }
    }
}