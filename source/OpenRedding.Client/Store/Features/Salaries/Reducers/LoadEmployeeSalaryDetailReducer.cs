namespace OpenRedding.Client.Store.Features.Salaries.Reducers
{
    using Fluxor;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaryDetail;
    using OpenRedding.Client.Store.State;

    public static class LoadEmployeeSalaryDetailReducer
    {
        [ReducerMethod]
        public static SalariesState ReduceLoadEmployeeSalaryDetailFromLinkAction(SalariesState state, LoadEmployeeSalaryDetailFromLinkAction action) =>
            new SalariesState(true, false, state.SalaryResults, state.SalaryDetail, state.SearchRequest);

        [ReducerMethod]
        public static SalariesState ReducerLoadEmployeeSalaryDetailFromLinkSuccessAction(SalariesState state, LoadEmployeeSalaryDetailSuccessAction action) =>
            new SalariesState(false, false, state.SalaryResults, action.SalaryDetail, state.SearchRequest);

        [ReducerMethod]
        public static SalariesState ReducerLoadEmployeeSalaryDetailFromLinkFailureAction(SalariesState state, LoadEmployeeSalaryDetailFailureAction action) =>
            new SalariesState(false, false, state.SalaryResults, null, state.SearchRequest, "An error has occurred");
    }
}
