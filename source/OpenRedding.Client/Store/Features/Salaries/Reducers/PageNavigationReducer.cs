namespace OpenRedding.Client.Store.Features.Salaries.Reducers
{
	using Fluxor;
    using OpenRedding.Client.Store.Features.Salaries.Actions.Navigation;

    public static class PageNavigationReducer
    {
        [ReducerMethod]
        public static SalariesState ReduceNavigateToPageAction(SalariesState state, NavigateToPageAction action) =>
            new SalariesState(true, false, state.SalaryResults, state.SalaryDetail, state.SearchRequest);

        [ReducerMethod]
        public static SalariesState ReduceNavigateToPageSuccessAction(SalariesState state, NavigateToPageSuccessAction action) =>
            new SalariesState(false, false, state.SalaryResults, state.SalaryDetail, state.SearchRequest);
    }
}
