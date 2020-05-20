namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries
{
    using OpenRedding.Domain.Salaries.Dtos;

    public class LoadEmployeeSalariesAction
    {
        public LoadEmployeeSalariesAction(EmployeeSalarySearchRequestDto? searchRequest, bool isTableRefresh = false) =>
            (SearchRequest, IsTableRefresh) = (searchRequest, isTableRefresh);

        public EmployeeSalarySearchRequestDto? SearchRequest { get; }

        public bool IsTableRefresh { get; }
    }
}
