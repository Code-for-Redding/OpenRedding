namespace OpenRedding.Core.Salaries.Queries.GetEmployeeSalaries
{
    using Common;
    using Domain.Salaries.ViewModels;

    public class GetEmployeeSalariesQuery : IOpenReddingRequest<EmployeeSearchResultList>
    {
        public GetEmployeeSalariesQuery(string? name, string? jobTitle, string? agency, string? status) =>
            (Name, JobTitle, Agency, Status) = (name, jobTitle, agency, status);

        public string? Name { get; }

        public string? JobTitle { get; }

        public string? Agency { get; }

        public string? Status { get; }
    }
}