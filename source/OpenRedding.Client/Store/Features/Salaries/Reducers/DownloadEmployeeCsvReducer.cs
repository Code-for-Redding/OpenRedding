namespace OpenRedding.Client.Store.Features.Salaries.Reducers
{
    using Fluxor;
    using OpenRedding.Client.Store;
    using OpenRedding.Client.Store.Features.Salaries.Actions.DownloadSalaryCsv;

    public static class DownloadEmployeeCsvReducer
    {
        [ReducerMethod]
        public static OpenReddingAppState ReduceDownloadSalaryCsvAction(OpenReddingAppState state, DownloadSalaryCsvAction action) =>
            new OpenReddingAppState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, state.SearchRequest);

        [ReducerMethod]
        public static OpenReddingAppState ReduceDownloadSalaryCsvSuccessAction(OpenReddingAppState state, DownloadSalaryCsvSuccessAction action) =>
            new OpenReddingAppState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, state.SearchRequest);

        [ReducerMethod]
        public static OpenReddingAppState ReduceDownloadSalaryCsvFailureAction(OpenReddingAppState state, DownloadSalaryCsvFailureAction action) =>
            new OpenReddingAppState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, state.SearchRequest, "An error has occurred");
    }
}
