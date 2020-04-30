namespace OpenRedding.Domain.Salaries.Dtos
{
    public class EmployeeSalarySearchRequestDto
    {
        public EmployeeSalarySearchRequestDto(string? name, string? jobTitle, string? agency, string? status, string? sortBy)
        {
            Name = name;
            JobTitle = jobTitle;
            Agency = agency;
            Status = status;
            SortBy = sortBy;
        }

        public string? Name { get; }

        public string? JobTitle { get; }

        public string? Agency { get; }

        public string? Status { get; }

        public string? SortBy { get; }
    }
}
