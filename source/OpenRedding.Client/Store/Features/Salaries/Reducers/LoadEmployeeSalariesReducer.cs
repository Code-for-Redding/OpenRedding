namespace OpenRedding.Client.Store.Features.Salaries.Reducers
{
    using Fluxor;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;
    using OpenRedding.Client.Store.State;
    using OpenRedding.Domain.Salaries.Dtos;

    public static class LoadEmployeeSalariesReducer
    {
        [ReducerMethod]
        public static SalariesState ReduceLoadEmployeeSalariesAction(SalariesState state, LoadEmployeeSalariesAction action) =>
            new SalariesState(!action.IsTableRefresh, action.IsTableRefresh, state.SalaryResults, state.SalaryDetail, action.SearchRequest);

        [ReducerMethod]
        public static SalariesState ReduceLoadEmployeeSalariesFromLinkAction(SalariesState state, LoadEmployeeSalariesFromLinkAction action) =>
            new SalariesState(false, true, state.SalaryResults, state.SalaryDetail, state.SearchRequest);

        [ReducerMethod]
        public static SalariesState ReduceLoadEmployeeSalariesSuccessAction(SalariesState state, LoadEmployeeSalariesSuccessAction action) =>
            new SalariesState(false, false, action.Response, state.SalaryDetail, state.SearchRequest);

        [ReducerMethod]
        public static SalariesState ReduceLoadEmployeeSalariesFailureAction(SalariesState state, LoadEmployeeSalariesFailureAction action) =>
            new SalariesState(false, false, null, state.SalaryDetail, state.SearchRequest, "An error has occurred");

        [ReducerMethod]
        public static SalariesState ReduceLoadEmployeeSalariesOnSearchClickedAction(SalariesState state, LoadEmployeesOnSearchClickedAction action)
        {
            if (state.SearchRequest is null)
            {
                return new SalariesState(false, true, state.SalaryResults, state.SalaryDetail, new EmployeeSalarySearchRequestDto(name: action.Name, jobTitle: action.JobTitle));
            }

            var updatedSearchRequest = new EmployeeSalarySearchRequestDto(
                name: action.Name,
                jobTitle: action.JobTitle,
                agency: state.SearchRequest.Agency,
                status: state.SearchRequest.Status,
                sortBy: state.SearchRequest.SortBy,
                year: state.SearchRequest.Year,
                sortField: state.SearchRequest.SortField,
                basePayRange: state.SearchRequest.BasePayRange,
                totalPayRange: state.SearchRequest.TotalPayRange);

            return new SalariesState(false, true, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
        }
    }
}
