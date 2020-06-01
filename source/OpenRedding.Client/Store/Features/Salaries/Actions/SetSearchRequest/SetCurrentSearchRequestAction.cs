namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
    using OpenRedding.Domain.Salaries.Dtos;

    public class SetCurrentSearchRequestAction
    {
        public SetCurrentSearchRequestAction(EmployeeSalarySearchRequestDto? searchRequest, bool isRefreshTable) =>
            (SearchRequest, IsRefreshTable) = (searchRequest, isRefreshTable);

        public EmployeeSalarySearchRequestDto? SearchRequest { get; }

        public bool IsRefreshTable { get; }
    }
}
