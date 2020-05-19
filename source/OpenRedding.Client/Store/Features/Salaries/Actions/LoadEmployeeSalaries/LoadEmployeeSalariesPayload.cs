namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries
{
    using OpenRedding.Client.Store.Common;
    using OpenRedding.Domain.Salaries.Dtos;

    public class LoadEmployeeSalariesPayload
    {
        public LoadEmployeeSalariesPayload(EmployeeSalarySearchRequestDto? searchRequest, bool isTableRefresh = false) =>
            (SearchRequest, IsTableRefresh) = (searchRequest, isTableRefresh);

        public EmployeeSalarySearchRequestDto? SearchRequest { get; }

        public bool IsTableRefresh { get; }
    }
}
