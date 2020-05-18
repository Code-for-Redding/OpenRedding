namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
    using OpenRedding.Domain.Salaries.Dtos;

    public class SetCurrentSearchRequestAction
    {
        public SetCurrentSearchRequestAction(EmployeeSalarySearchRequestDto? searchRequest) =>
            SearchRequest = searchRequest;

        public EmployeeSalarySearchRequestDto? SearchRequest { get; }
    }
}
