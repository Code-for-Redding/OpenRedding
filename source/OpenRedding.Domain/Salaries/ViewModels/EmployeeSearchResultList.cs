namespace OpenRedding.Domain.Salaries.ViewModels
{
    using System.Collections.Generic;
    using Common.ViewModels;
    using Dtos;

    public class EmployeeSearchResultList : OpenReddingViewModelList
    {
        public EmployeeSearchResultList(IEnumerable<EmployeeSalarySearchDto> employees, int totalResults)
            : base(totalResults)
        {
            Employees = employees;
        }

        public IEnumerable<EmployeeSalarySearchDto> Employees { get; }
    }
}
