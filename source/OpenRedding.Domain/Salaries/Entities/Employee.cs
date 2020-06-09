namespace OpenRedding.Domain.Salaries.Entities
{
    using OpenRedding.Domain.Salaries.Enums;

    /// <summary>
    /// Represents a salaried employee based on the CSV data returned from Transparent California.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets primary key for the stored entity.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the employee first name.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the employee middle name.
        /// </summary>
        public string? MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the employee last name.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the employee job title.
        /// </summary>
        public string? JobTitle { get; set; }

        /// <summary>
        /// Gets or sets the employee base pay.
        /// </summary>
        public decimal BasePay { get; set; }

        /// <summary>
        /// Gets or sets the employee overtime pay.
        /// </summary>
        public decimal OvertimePay { get; set; }

        /// <summary>
        /// Gets or sets the employee other pay.
        /// </summary>
        public decimal OtherPay { get; set; }

        /// <summary>
        /// Gets or sets the employee benefit compensation pay.
        /// </summary>
        public decimal Benefits { get; set; }

        /// <summary>
        /// Gets or sets the total aggregate employee pay.
        /// </summary>
        public decimal TotalPay { get; set; }

        /// <summary>
        /// Gets or sets the total pension pay entitles to the employee.
        /// </summary>
        public decimal? PensionDebt { get; set; }

        /// <summary>
        /// Gets or sets the total aggregate employee pay including benefits.
        /// </summary>
        public decimal TotalPayWithBenefits { get; set; }

        /// <summary>
        /// Gets or sets the reporting year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets any additional notes on employee pay.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the employee agency.
        /// </summary>
        public EmployeeAgency EmployeeAgency { get; set; }

        /// <summary>
        /// Gets or sets the employee employment status.
        /// </summary>
        public EmployeeStatus EmployeeStatus { get; set; }
    }
}
