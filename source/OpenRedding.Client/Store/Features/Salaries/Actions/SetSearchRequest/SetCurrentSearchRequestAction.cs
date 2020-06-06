namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
    using OpenRedding.Domain.Salaries.Dtos;

    public class SetCurrentSearchRequestAction
    {
        public SetCurrentSearchRequestAction(EmployeeSalarySearchRequestDto? searchRequest, bool isRefreshTable, bool loadFromApi) =>
            (SearchRequest, IsRefreshTable, LoadFromApi) = (searchRequest, isRefreshTable, loadFromApi);

        public EmployeeSalarySearchRequestDto? SearchRequest { get; }

        public bool IsRefreshTable { get; }

        public bool LoadFromApi { get; }
    }
}
