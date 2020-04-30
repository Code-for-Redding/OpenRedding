namespace OpenRedding.Domain.Salaries.Dtos
{
    public class EmployeeSalarySearchResultDto
    {
        public EmployeeSalarySearchResultDto(int id, string? name, string? jobTitle, string? agency, string? status, int year, decimal basePay, decimal totalPayWithBenefits)
        {
            Id = id;
            Name = name;
            JobTitle = jobTitle;
            Agency = agency;
            Status = status;
            Year = year;
            BasePay = basePay;
            TotalPayWithBenefits = totalPayWithBenefits;
        }

        public int Id { get; }

        public string? Name { get; }

        public string? JobTitle { get; }

        public string? Agency { get; }

        public string? Status { get; }

        public int Year { get; set; }

        public decimal BasePay { get; }

        public decimal TotalPayWithBenefits { get; }
    }
}
