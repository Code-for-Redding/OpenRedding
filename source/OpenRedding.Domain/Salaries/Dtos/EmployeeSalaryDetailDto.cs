namespace OpenRedding.Domain.Salaries.Dtos
{
	using OpenRedding.Domain.Common.Miscellaneous;

    /// <summary>
    /// Represents an employee returned from the application layer.
    /// </summary>
    public class EmployeeSalaryDetailDto
    {
        /// <summary>
        /// Gets or sets the employee ID from the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the employee name, both first and last.
        /// </summary>
        public string? Name { get; set; }

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
        /// Gets or sets the total aggregate employee pay including benefits.
        /// </summary>
		public decimal TotalPayWithBenefits { get; set; }

        /// <summary>
        /// Gets or sets the reporting year.
        /// </summary>
		public int Year { get; set; }

        /// <summary>
        /// Gets or sets the employee agency.
        /// </summary>
		public string? Agency { get; set; }

        /// <summary>
        /// Gets or sets the employee employment status.
        /// </summary>
		public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the employee self link.
        /// </summary>
        public OpenReddingLink? Self { get; set; }
    }
}
