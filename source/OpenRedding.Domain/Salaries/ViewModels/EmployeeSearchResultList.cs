namespace OpenRedding.Domain.Salaries.ViewModels
{
	using System.Collections.Generic;
	using System.Linq;
	using Dtos;

	public class EmployeeSearchResultList
	{
		public EmployeeSearchResultList(IEnumerable<EmployeeSalarySearchDto> employees) => Employees = employees;

		public IEnumerable<EmployeeSalarySearchDto> Employees { get; }

		public int Count => Employees.Count();
	}
}