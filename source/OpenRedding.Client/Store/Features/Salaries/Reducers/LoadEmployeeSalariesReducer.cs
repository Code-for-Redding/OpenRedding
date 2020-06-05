namespace OpenRedding.Client.Store.Features.Salaries.Reducers
{
    using Fluxor;
    using OpenRedding.Client.Store;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;
    using OpenRedding.Domain.Salaries.Dtos;

    public static class LoadEmployeeSalariesReducer
    {
        [ReducerMethod]
        public static OpenReddingAppState ReduceLoadEmployeeSalariesAction(OpenReddingAppState state, LoadEmployeeSalariesAction action) =>
            new OpenReddingAppState(!action.IsTableRefresh, action.IsTableRefresh, state.SalaryResults, state.SalaryDetail, action.SearchRequest);

        [ReducerMethod]
        public static OpenReddingAppState ReduceLoadEmployeeSalariesFromLinkAction(OpenReddingAppState state, LoadEmployeeSalariesFromLinkAction action) =>
            new OpenReddingAppState(false, true, state.SalaryResults, state.SalaryDetail, state.SearchRequest);

        [ReducerMethod]
        public static OpenReddingAppState ReduceLoadEmployeeSalariesSuccessAction(OpenReddingAppState state, LoadEmployeeSalariesSuccessAction action) =>
            new OpenReddingAppState(false, false, action.Response, state.SalaryDetail, state.SearchRequest);

        [ReducerMethod]
        public static OpenReddingAppState ReduceLoadEmployeeSalariesFailureAction(OpenReddingAppState state, LoadEmployeeSalariesFailureAction action) =>
            new OpenReddingAppState(false, false, null, state.SalaryDetail, state.SearchRequest, "An error has occurred");

        [ReducerMethod]
        public static OpenReddingAppState ReduceLoadEmployeeSalariesOnSearchClickedAction(OpenReddingAppState state, LoadEmployeesOnSearchClickedAction action)
        {
            if (state.SearchRequest is null)
            {
                return new OpenReddingAppState(false, true, state.SalaryResults, state.SalaryDetail, new EmployeeSalarySearchRequestDto(name: action.Name, jobTitle: action.JobTitle));
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

            return new OpenReddingAppState(false, true, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
        }
    }
}
