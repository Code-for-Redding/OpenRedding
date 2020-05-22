namespace OpenRedding.Domain.Salaries.ViewModels
{
    using System.Collections.Generic;
    using Dtos;

    public class EmployeeSalaryDetailViewModel
    {
        /*
        public EmployeeSalaryDetailViewModel(EmployeeSalaryDetailDto employee, IEnumerable<RelatedEmployeeDetailDto>? relatedRecords = null) =>
            (Employee, RelatedRecords) = (employee, relatedRecords);
        */

        public EmployeeSalaryDetailDto? Employee { get; set; }

        public IEnumerable<RelatedEmployeeDetailDto>? RelatedRecords { get; set; }
    }
}
