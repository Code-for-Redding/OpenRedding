namespace OpenRedding.Client.Services
{
    using Fluxor;
    using OpenRedding.Client.Store.Features.Salaries;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;
    using OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.Enums;

    /// <summary>
    /// A state service that allows components to interact with state without having knowledge of what action utilize.
    /// Provides separation from the store and our components, so that our components will not require references to store actions.
    /// </summary>
    public class SalariesStateFacade
    {
        private readonly IDispatcher _dispatcher;
        private readonly IState<SalariesState> _state;

        public SalariesStateFacade(IDispatcher dispatcher, IState<SalariesState> state) =>
            (_dispatcher, _state) = (dispatcher, state);

        public void LoadAllEmployees() =>
            _dispatcher.Dispatch(new LoadEmployeeSalariesAction(null));

        public void LoadEmployeesFromSearchRequest(bool isTableRefresh = false) =>
            _dispatcher.Dispatch(new LoadEmployeeSalariesAction(_state.Value.SearchRequest, isTableRefresh));

        public void LoadEmployeesFromLink(string link) =>
            _dispatcher.Dispatch(new LoadEmployeeSalariesFromLinkAction(link));

        public void SetCurrentSearchRequest(EmployeeSalarySearchRequestDto? searchRequest) =>
            _dispatcher.Dispatch(new SetCurrentSearchRequestAction(searchRequest));

        public void SetEmployeeName(string name) =>
            _dispatcher.Dispatch(new SetEmployeeNameAction(name));

        public void SetEmployeeJobTitle(string title) =>
            _dispatcher.Dispatch(new SetEmployeeJobTitleAction(title));

        public void SetEmployeeAgency(EmployeeAgency agency, bool loadFromApi = false) =>
            _dispatcher.Dispatch(new SetEmployeeAgencyAction(agency, loadFromApi));

        public void SetEmployeeStatus(EmployeeStatus status, bool loadFromApi = false) =>
            _dispatcher.Dispatch(new SetEmployeeStatusAction(status, loadFromApi));

        public void SetEmploymentYear(EmploymentYear year) =>
            _dispatcher.Dispatch(new SetEmploymentYearAction(year));

        public void SetSalarySearchBaseRange(SalarySearchRange range) =>
            _dispatcher.Dispatch(new SetSalarySearchBaseRangeAction(range));

        public void SetSalarySearchTotalRange(SalarySearchRange range) =>
            _dispatcher.Dispatch(new SetSalarySearchTotalRangeAction(range));

        public void SetSalarySortField(SalarySortField field) =>
            _dispatcher.Dispatch(new SetSalarySortFieldAction(field));

        public void SetSalarySortBy(SalarySortByOption option) =>
            _dispatcher.Dispatch(new SetSalarySortByAction(option));
    }
}
