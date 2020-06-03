namespace OpenRedding.Client.Store.Features.Salaries.Reducers
{
	using Fluxor;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaryDetail;

    public static class LoadEmployeeSalaryDetailReducer
    {
        [ReducerMethod]
        public static OpenReddingAppState ReduceLoadEmployeeSalaryDetailFromLinkAction(OpenReddingAppState state, LoadEmployeeSalaryDetailFromLinkAction action) =>
            new OpenReddingAppState(true, false, state.SalaryResults, state.SalaryDetail, state.SearchRequest);

        [ReducerMethod]
        public static OpenReddingAppState ReducerLoadEmployeeSalaryDetailFromLinkSuccessAction(OpenReddingAppState state, LoadEmployeeSalaryDetailSuccessAction action) =>
            new OpenReddingAppState(false, false, state.SalaryResults, action.SalaryDetail, state.SearchRequest);

        [ReducerMethod]
        public static OpenReddingAppState ReducerLoadEmployeeSalaryDetailFromLinkFailureAction(OpenReddingAppState state, LoadEmployeeSalaryDetailFailureAction action) =>
            new OpenReddingAppState(false, false, state.SalaryResults, null, state.SearchRequest);
    }
}
