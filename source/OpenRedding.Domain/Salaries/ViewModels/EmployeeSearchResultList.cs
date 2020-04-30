namespace OpenRedding.Domain.Salaries.ViewModels
{
    using System.Collections.Generic;
    using Common.ViewModels;
    using Dtos;

    public class EmployeeSearchResultList : OpenReddingViewModelList
    {
        public EmployeeSearchResultList(IEnumerable<EmployeeSalarySearchResultDto> employees, int totalResults)
            : base(totalResults)
        {
            Employees = employees;
        }

        public IEnumerable<EmployeeSalarySearchResultDto> Employees { get; }
    }
}
