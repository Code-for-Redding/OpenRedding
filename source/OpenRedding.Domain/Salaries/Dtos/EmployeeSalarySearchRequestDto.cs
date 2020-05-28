namespace OpenRedding.Domain.Salaries.Dtos
{
    public class EmployeeSalarySearchRequestDto
    {
        public EmployeeSalarySearchRequestDto()
        {
        }

        public EmployeeSalarySearchRequestDto(
            string? name = null,
            string? jobTitle = null,
            string? agency = null,
            string? status = null,
            string? sortBy = null,
            int? year = null,
            string? sortField = null,
            int? basePayRange = null,
            int? totalPayRange = null)
        {
            Name = name;
            JobTitle = jobTitle;
            Agency = agency;
            Status = status;
            SortBy = sortBy;
            Year = year;
            SortField = sortField;
            BasePayRange = basePayRange;
            TotalPayRange = totalPayRange;
        }

        public string? Name { get; }

        public string? JobTitle { get; }

        public string? Agency { get; }

        public string? Status { get; }

        public string? SortBy { get; }

        public int? Year { get; }

        public string? SortField { get; }

        public int? BasePayRange { get; }

        public int? TotalPayRange { get; }
    }
}
