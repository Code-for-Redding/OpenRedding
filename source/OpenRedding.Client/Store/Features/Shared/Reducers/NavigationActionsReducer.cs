namespace OpenRedding.Client.Store.Features.Shared.Reducers
{
    using Fluxor;
    using Fluxor.Blazor.Web.Middlewares.Routing;
    using OpenRedding.Client.Store.State;

    public static class NavigationActionsReducer
    {
        [ReducerMethod]
        public static SalariesState ReduceNaviationAction(SalariesState state, GoAction action) =>
            new SalariesState(state.IsLoading, null, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, state.SearchRequest);
    }
}
