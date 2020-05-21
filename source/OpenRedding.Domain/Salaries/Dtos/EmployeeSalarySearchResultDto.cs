namespace OpenRedding.Domain.Salaries.Dtos
{
	using OpenRedding.Domain.Common.Miscellaneous;

    public class EmployeeSalarySearchResultDto
    {
        public EmployeeSalarySearchResultDto(
            int id,
            string? name,
            string? jobTitle,
            string? agency,
            string? status,
            int year,
            decimal basePay,
            decimal totalPayWithBenefits,
            OpenReddingLink link)
        {
            Id = id;
            Name = name;
            JobTitle = jobTitle;
            Agency = agency;
            Status = status;
            Year = year;
            BasePay = basePay;
            TotalPayWithBenefits = totalPayWithBenefits;
            EmployeeDetailLink = link;
        }

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? JobTitle { get; set; }

        public string? Agency { get; set; }

        public string? Status { get; set; }

        public int Year { get; set; }

        public decimal BasePay { get; set; }

        public decimal TotalPayWithBenefits { get; set; }

        public OpenReddingLink? EmployeeDetailLink { get; set; }
    }
}
