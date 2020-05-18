namespace OpenRedding.Client.Store.Features.Salaries.Reducers
{
	using Fluxor;
	using OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest;
	using OpenRedding.Domain.Salaries.Dtos;

	internal enum SalaryDropdownContext
	{
		Agency,
		Status
	}

	public static class SetSearchRequestReducer
    {
		[ReducerMethod]
		public static SalariesState SetCurrentSearchRequestActionReducer(SalariesState state, SetCurrentSearchRequestAction action) =>
			new SalariesState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, action.SearchRequest);

		[ReducerMethod]
		public static SalariesState SetEmployeeAgencyActionReducer(SalariesState state, SetEmployeeAgencyAction action)
		{
			if (state.SearchRequest is null)
			{
				return InitializeSearchRequest(state, SalaryDropdownContext.Agency, action.Agency.ToString());
			}

			var updatedSearchRequest = new EmployeeSalarySearchRequestDto(
				state.SearchRequest.Name,
				state.SearchRequest.JobTitle,
				action.Agency.ToString(),
				state.SearchRequest.Status,
				state.SearchRequest.SortBy,
				state.SearchRequest.Year,
				state.SearchRequest.SortField);

			return new SalariesState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
		}

		[ReducerMethod]
		public static SalariesState SetEmployeeStatusActionReducer(SalariesState state, SetEmployeeStatusAction action)
		{
			if (state.SearchRequest is null)
			{
				return InitializeSearchRequest(state, SalaryDropdownContext.Agency, action.Status.ToString());
			}

			var updatedSearchRequest = new EmployeeSalarySearchRequestDto(
				state.SearchRequest.Name,
				state.SearchRequest.JobTitle,
				state.SearchRequest.Agency,
				action.Status.ToString(),
				state.SearchRequest.SortBy,
				state.SearchRequest.Year,
				state.SearchRequest.SortField);

			return new SalariesState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
		}

		private static SalariesState InitializeSearchRequest(SalariesState state, SalaryDropdownContext context, string value)
		{
			var searchRequest = context switch
			{
				SalaryDropdownContext.Agency => new EmployeeSalarySearchRequestDto(null, null, value, null, null, null, null),
				SalaryDropdownContext.Status => new EmployeeSalarySearchRequestDto(null, null, null, value, null, null, null),
				_ => new EmployeeSalarySearchRequestDto(null, null, null, null, null, null, null)
			};

			return new SalariesState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, searchRequest);
		}
    }
}
