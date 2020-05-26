namespace OpenRedding.Domain.Salaries.Dtos
{
    using OpenRedding.Domain.Salaries.Enums;

    public class EmployeeSalaryExportDto
    {
        public EmployeeSalaryExportDto(
            string employeeName,
            string jobTitle,
            decimal basePay,
            decimal overtimePay,
            decimal otherPay,
            decimal benefits,
            decimal totalPay,
            decimal pensionDebt,
            decimal totalPayWithBenefits,
            int year,
            EmployeeAgency employeeAgency,
            EmployeeStatus employeeStatus)
        {
            EmployeeName = employeeName;
            JobTitle = jobTitle;
            BasePay = basePay;
            OvertimePay = overtimePay;
            OtherPay = otherPay;
            Benefits = benefits;
            TotalPay = totalPay;
            PensionDebt = pensionDebt;
            TotalPayWithBenefits = totalPayWithBenefits;
            Year = year;
            EmployeeAgency = employeeAgency;
            EmployeeStatus = employeeStatus;
        }

        /// <summary>
        /// Gets the employee name, both first and last.
        /// </summary>
        public string EmployeeName { get; }

        /// <summary>
        /// Gets the employee job title.
        /// </summary>
        public string JobTitle { get; }

        /// <summary>
        /// Gets the employee base pay.
        /// </summary>
        public decimal BasePay { get; }

        /// <summary>
        /// Gets the employee overtime pay.
        /// </summary>
        public decimal OvertimePay { get; }

        /// <summary>
        /// Gets the employee other pay.
        /// </summary>
        public decimal OtherPay { get; }

        /// <summary>
        /// Gets the employee benefit compensation pay.
        /// </summary>
        public decimal Benefits { get; }

        /// <summary>
        /// Gets the total aggregate employee pay.
        /// </summary>
        public decimal TotalPay { get; }

        /// <summary>
        /// Gets the total pension pay entitles to the employee.
        /// </summary>
        public decimal PensionDebt { get; }

        /// <summary>
        /// Gets the total aggregate employee pay including benefits.
        /// </summary>
        public decimal TotalPayWithBenefits { get; }

        /// <summary>
        /// Gets the reporting year.
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// Gets the employee agency.
        /// </summary>
        public EmployeeAgency EmployeeAgency { get; }

        /// <summary>
        /// Gets the employee employment status.
        /// </summary>
        public EmployeeStatus EmployeeStatus { get; }
    }
}
