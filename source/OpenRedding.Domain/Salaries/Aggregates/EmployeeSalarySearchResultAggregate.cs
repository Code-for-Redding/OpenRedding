namespace OpenRedding.Domain.Salaries.Aggregates
{
	using System.Collections.Generic;
	using OpenRedding.Domain.Salaries.Dtos;
	using OpenRedding.Shared;

    public class EmployeeSalarySearchResultAggregate
    {
        public EmployeeSalarySearchResultAggregate(IEnumerable<EmployeeSalarySearchResultDto> results, int count) =>
            (Results, Count) = (results, count);

        public IEnumerable<EmployeeSalarySearchResultDto> Results { get; }

        public int Count { get; }

        public int Pages => (Count / OpenReddingConstants.MaxPageSizeResult) + 1;
    }
}
