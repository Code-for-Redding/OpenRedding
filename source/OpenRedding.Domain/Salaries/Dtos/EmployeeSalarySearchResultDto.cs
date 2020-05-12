namespace OpenRedding.Domain.Salaries.Dtos
{
    public class EmployeeSalarySearchResultDto
    {
        public EmployeeSalarySearchResultDto()
        {
        }

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

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? JobTitle { get; set; }

        public string? Agency { get; set; }

        public string? Status { get; set; }

        public int Year { get; set; }

        public decimal BasePay { get; set; }

        public decimal TotalPayWithBenefits { get; set; }
    }
}
