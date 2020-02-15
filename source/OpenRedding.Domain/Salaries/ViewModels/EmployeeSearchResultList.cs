namespace OpenRedding.Domain.Salaries.ViewModels
{
	using System.Collections.Generic;
	using Common.ViewModels;
	using Dtos;

	public class EmployeeSearchResultList : OpenReddingViewModelList
	{
		public EmployeeSearchResultList(IReadOnlyList<EmployeeSalarySearchDto> employees)
			: base(employees)
		{
			Employees = employees;
		}

		public IEnumerable<EmployeeSalarySearchDto> Employees { get; }
	}
}