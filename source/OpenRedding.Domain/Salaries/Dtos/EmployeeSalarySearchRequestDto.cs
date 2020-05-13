namespace OpenRedding.Domain.Salaries.Dtos
{
    public class EmployeeSalarySearchRequestDto
    {
        public EmployeeSalarySearchRequestDto(string? name, string? jobTitle, string? agency, string? status, string? sortBy, int? year, string? sortField)
        {
            Name = name;
            JobTitle = jobTitle;
            Agency = agency;
            Status = status;
            SortBy = sortBy;
            Year = year;
            SortField = sortField;
        }

        public string? Name { get; }

        public string? JobTitle { get; }

        public string? Agency { get; }

        public string? Status { get; }

        public string? SortBy { get; }

        public int? Year { get; }

        public string? SortField { get; }
    }
}
