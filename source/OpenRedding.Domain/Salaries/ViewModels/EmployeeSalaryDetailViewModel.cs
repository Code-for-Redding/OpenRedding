namespace OpenRedding.Domain.Salaries.ViewModels
{
    using System.Collections.Generic;
    using Dtos;

    public class EmployeeSalaryDetailViewModel
    {
        public EmployeeSalaryDetailDto? Employee { get; set; }

        public IEnumerable<RelatedEmployeeDetailDto>? RelatedRecords { get; set; }

        public decimal? OccupationalBasePayAverage { get; set; }

        public decimal? OccupationalTotalPayAverage { get; set; }
    }
}
