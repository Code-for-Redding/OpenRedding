namespace OpenRedding.Domain.Salaries.Dtos
{
    public class TransparentCaliforniaCsvReadEmployeeDto
    {
        public string? EmployeeName { get; set; }

        public string? JobTitle { get; set; }

        public decimal BasePay { get; set; }

        public decimal OvertimePay { get; set; }

        public decimal OtherPay { get; set; }

        public decimal Benefits { get; set; }

        public decimal TotalPay { get; set; }

        public decimal? PensionDebt { get; set; }

        public decimal TotalPayWithBenefits { get; set; }

        public int Year { get; set; }

        public string? Notes { get; set; }

        public string? EmployeeAgency { get; set; }

        public string? EmployeeStatus { get; set; }
    }
}
