namespace OpenRedding.Domain.Salaries.ViewModels
{
    using System.Collections.Generic;
    using Dtos;

    public class EmployeeSalaryDetailViewModel
    {
        public EmployeeSalaryDetailDto? Employee { get; set; }

        public IEnumerable<RelatedEmployeeDetailDto>? RelatedRecords { get; set; }

        public decimal? OccupationalBasePayAverage { get; set; }

        public string? BasePayDelta { get; set; }

        public decimal? OccupationalTotalPayAverage { get; set; }

        public string? TotalPayDelta { get; set; }

        public decimal? OccupationalBenefitsAverage { get; set; }

        public string? BenefitsDelta { get; set; }
    }
}
