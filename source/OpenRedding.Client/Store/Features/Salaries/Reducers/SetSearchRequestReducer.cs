namespace OpenRedding.Client.Store.Features.Salaries.Reducers
{
	using Fluxor;
	using OpenRedding.Client.Components.Dropdowns.Salaries;
	using OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest;
	using OpenRedding.Domain.Salaries.Dtos;
	using OpenRedding.Domain.Salaries.Enums;

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
				return InitializeSearchRequest(state, SalarySearchDropdownContext.Agency, action.Agency.ToString());
			}

			var updatedSearchRequest = new EmployeeSalarySearchRequestDto(
				name: state.SearchRequest.Name,
				jobTitle: state.SearchRequest.JobTitle,
				agency: action.Agency.ToString(),
				status: state.SearchRequest.Status,
				sortBy: state.SearchRequest.SortBy,
				year: state.SearchRequest.Year,
				sortField: state.SearchRequest.SortField,
				basePayRange: state.SearchRequest.BasePayRange,
				totalPayRange: state.SearchRequest.TotalPayRange);

			return new SalariesState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
		}

		[ReducerMethod]
		public static SalariesState SetEmployeeStatusActionReducer(SalariesState state, SetEmployeeStatusAction action)
		{
			if (state.SearchRequest is null)
			{
				return InitializeSearchRequest(state, SalarySearchDropdownContext.Status, action.Status.ToString());
			}

			var updatedSearchRequest = new EmployeeSalarySearchRequestDto(
				name: state.SearchRequest.Name,
				jobTitle: state.SearchRequest.JobTitle,
				agency: state.SearchRequest.Agency,
				status: action.Status.ToString(),
				sortBy: state.SearchRequest.SortBy,
				year: state.SearchRequest.Year,
				sortField: state.SearchRequest.SortField,
				basePayRange: state.SearchRequest.BasePayRange,
				totalPayRange: state.SearchRequest.TotalPayRange);

			return new SalariesState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
		}

		[ReducerMethod]
		public static SalariesState SetEmploymentYearActionReducer(SalariesState state, SetEmploymentYearAction action)
		{
			if (state.SearchRequest is null)
			{
				return InitializeSearchRequest(state, SalarySearchDropdownContext.Year, ((int)action.Year).ToString());
			}

			var updatedSearchRequest = new EmployeeSalarySearchRequestDto(
				name: state.SearchRequest.Name,
				jobTitle: state.SearchRequest.JobTitle,
				agency: state.SearchRequest.Agency,
				status: state.SearchRequest.Status,
				sortBy: state.SearchRequest.SortBy,
				year: (int)action.Year,
				sortField: state.SearchRequest.SortField,
				basePayRange: state.SearchRequest.BasePayRange,
				totalPayRange: state.SearchRequest.TotalPayRange);

			return new SalariesState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
		}

		[ReducerMethod]
		public static SalariesState SetSalarySearchBaseRangeActionReducer(SalariesState state, SetSalarySearchBaseRangeAction action)
		{
			if (state.SearchRequest is null)
			{
				return InitializeSearchRequest(state, SalarySearchDropdownContext.BasePayRange, ((int)action.Range).ToString());
			}

			var updatedSearchRequest = new EmployeeSalarySearchRequestDto(
				name: state.SearchRequest.Name,
				jobTitle: state.SearchRequest.JobTitle,
				agency: state.SearchRequest.Agency,
				status: state.SearchRequest.Status,
				sortBy: state.SearchRequest.SortBy,
				year: state.SearchRequest.Year,
				sortField: state.SearchRequest.SortField,
				basePayRange: (int)action.Range,
				totalPayRange: state.SearchRequest.TotalPayRange);

			return new SalariesState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
		}

		[ReducerMethod]
		public static SalariesState SetSalarySearchTotalRangeActionReducer(SalariesState state, SetSalarySearchTotalRangeAction action)
		{
			if (state.SearchRequest is null)
			{
				return InitializeSearchRequest(state, SalarySearchDropdownContext.TotalPayRange, ((int)action.Range).ToString());
			}

			var updatedSearchRequest = new EmployeeSalarySearchRequestDto(
				name: state.SearchRequest.Name,
				jobTitle: state.SearchRequest.JobTitle,
				agency: state.SearchRequest.Agency,
				status: state.SearchRequest.Status,
				sortBy: state.SearchRequest.SortBy,
				year: state.SearchRequest.Year,
				sortField: state.SearchRequest.SortField,
				basePayRange: state.SearchRequest.BasePayRange,
				totalPayRange: (int)action.Range);

			return new SalariesState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
		}

		[ReducerMethod]
		public static SalariesState SetSalarySortFieldActionReducer(SalariesState state, SetSalarySortFieldAction action)
		{
			var sortField = action.SortField.ToString();

			if (state.SearchRequest is null)
			{
				return InitializeSearchRequest(state, SalarySearchDropdownContext.SortField, sortField);
			}

			var updatedSearchRequest = new EmployeeSalarySearchRequestDto(
				name: state.SearchRequest.Name,
				jobTitle: state.SearchRequest.JobTitle,
				agency: state.SearchRequest.Agency,
				status: state.SearchRequest.Status,
				sortBy: state.SearchRequest.SortBy,
				year: state.SearchRequest.Year,
				sortField: sortField,
				basePayRange: state.SearchRequest.BasePayRange,
				totalPayRange: state.SearchRequest.TotalPayRange);

			return new SalariesState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
		}

		private static SalariesState InitializeSearchRequest(SalariesState state, SalarySearchDropdownContext context, string? value)
		{
			var searchRequest = context switch
			{
				SalarySearchDropdownContext.Agency => new EmployeeSalarySearchRequestDto(agency: value),
				SalarySearchDropdownContext.Status => new EmployeeSalarySearchRequestDto(status: value),
				SalarySearchDropdownContext.Year => new EmployeeSalarySearchRequestDto(year: int.TryParse(value, out var parsedValue) ? parsedValue : (int)EmploymentYear.AllYears),
				SalarySearchDropdownContext.BasePayRange => new EmployeeSalarySearchRequestDto(basePayRange: int.TryParse(value, out var parsedValue) ? parsedValue : (int)SalarySearchRange.AllSalaries),
				SalarySearchDropdownContext.TotalPayRange => new EmployeeSalarySearchRequestDto(totalPayRange: int.TryParse(value, out var parsedValue) ? parsedValue : (int)SalarySearchRange.AllSalaries),
				SalarySearchDropdownContext.SortField => new EmployeeSalarySearchRequestDto(sortField: value),
				_ => new EmployeeSalarySearchRequestDto()
			};

			return new SalariesState(state.IsLoading, state.IsTableRefresh, state.SalaryResults, state.SalaryDetail, searchRequest);
		}
    }
}
