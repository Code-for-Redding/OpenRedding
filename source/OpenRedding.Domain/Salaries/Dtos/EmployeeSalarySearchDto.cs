namespace OpenRedding.Domain.Salaries.Dtos
{
    public class EmployeeSalarySearchDto
    {
        public EmployeeSalarySearchDto(int id, string? name, string? agency, string? status, decimal basePay, decimal totalPayWithBenefits) =>
            (Id, Name, Agency, Status, BasePay, TotalPayWithBenefits) = (id, name, agency, status, basePay, totalPayWithBenefits);

        public int Id { get; }

        public string? Name { get; }

        public string? Agency { get; }

        public string? Status { get; }

        public decimal BasePay { get; }

        public decimal TotalPayWithBenefits { get; }
    }
}