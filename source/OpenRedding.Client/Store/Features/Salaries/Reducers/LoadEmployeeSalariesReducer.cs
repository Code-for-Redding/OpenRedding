namespace OpenRedding.Client.Store.Features.Salaries.Reducers
{
	using Fluxor;
	using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;

    public static class LoadEmployeeSalariesReducer
    {
        [ReducerMethod]
        public static SalariesState ReduceLoadEmployeeSalariesAction(SalariesState state, LoadEmployeeSalariesAction action) =>
            new SalariesState(!action.IsTableRefresh, action.IsTableRefresh, state.SalaryResults, state.SalaryDetail);

        [ReducerMethod]
        public static SalariesState ReduceLoadEmployeeSalariesSuccessAction(SalariesState state, LoadEmployeeSalariesSuccessAction action) =>
            new SalariesState(false, false, action.Salaries, state.SalaryDetail);

        [ReducerMethod]
        public static SalariesState ReduceLoadEmployeeSalariesFailureAction(SalariesState state, LoadEmployeeSalariesFailureAction action) =>
            new SalariesState(false, false, null, state.SalaryDetail);
    }
}
