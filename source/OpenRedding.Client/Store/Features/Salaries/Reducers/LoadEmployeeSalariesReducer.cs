namespace OpenRedding.Client.Store.Features.Salaries.Reducers
{
    using Fluxor;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalariesFromLink;

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
            new SalariesState(false, false, null, state.SalaryDetail, state.SearchRequest);

        /*
        [ReducerMethod]
        public static SalariesState ReduceLoadEmployees(SalariesState state, LoadEmployeeSalariesTypedAction action) =>
            new SalariesState(!action.Payload.IsTableRefresh, action.Payload.IsTableRefresh, state.SalaryResults, state.SalaryDetail, action.Payload.SearchRequest);
         */
    }
}
