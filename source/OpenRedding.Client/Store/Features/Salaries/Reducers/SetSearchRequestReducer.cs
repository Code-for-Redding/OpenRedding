namespace OpenRedding.Client.Store.Features.Salaries.Reducers
{
    using Fluxor;
    using OpenRedding.Client.Components.Dropdowns.Salaries;
    using OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest;
    using OpenRedding.Client.Store.State;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.Enums;

    public static class SetSearchRequestReducer
    {
        [ReducerMethod]
        public static SalariesState SetCurrentSearchRequestActionReducer(SalariesState state, SetCurrentSearchRequestAction action) =>
            new SalariesState(state.IsLoading, null, action.IsRefreshTable, state.SalaryResults, state.SalaryDetail, action.SearchRequest);

        [ReducerMethod]
        public static SalariesState SetEmployeeNameActionReducer(SalariesState state, SetEmployeeNameAction action)
        {
            if (state.SearchRequest is null)
            {
                return InitializeSearchRequest(state, SalarySearchContext.Name, action.Name);
            }

            var updatedSearchRequest = new EmployeeSalarySearchRequestDto(
                name: action.Name,
                jobTitle: state.SearchRequest.JobTitle,
                agency: state.SearchRequest.Agency,
                status: state.SearchRequest.Status,
                sortBy: state.SearchRequest.SortBy,
                year: state.SearchRequest.Year,
                sortField: state.SearchRequest.SortField,
                basePayRange: state.SearchRequest.BasePayRange,
                totalPayRange: state.SearchRequest.TotalPayRange);

            return new SalariesState(false, null, true, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
        }

        [ReducerMethod]
        public static SalariesState SetEmployeeJobTitleActionReducer(SalariesState state, SetEmployeeJobTitleAction action)
        {
            if (state.SearchRequest is null)
            {
                return InitializeSearchRequest(state, SalarySearchContext.JobTitle, action.JobTitle);
            }

            var updatedSearchRequest = new EmployeeSalarySearchRequestDto(
                name: state.SearchRequest.Name,
                jobTitle: action.JobTitle,
                agency: state.SearchRequest.Agency,
                status: state.SearchRequest.Status,
                sortBy: state.SearchRequest.SortBy,
                year: state.SearchRequest.Year,
                sortField: state.SearchRequest.SortField,
                basePayRange: state.SearchRequest.BasePayRange,
                totalPayRange: state.SearchRequest.TotalPayRange);

            return new SalariesState(false, null, true, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
        }

        [ReducerMethod]
        public static SalariesState SetEmployeeAgencyActionReducer(SalariesState state, SetEmployeeAgencyAction action)
        {
            if (state.SearchRequest is null)
            {
                return InitializeSearchRequest(state, SalarySearchContext.Agency, action.Agency.ToString());
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

            return new SalariesState(false, null, true, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
        }

        [ReducerMethod]
        public static SalariesState SetEmployeeStatusActionReducer(SalariesState state, SetEmployeeStatusAction action)
        {
            if (state.SearchRequest is null)
            {
                return InitializeSearchRequest(state, SalarySearchContext.Status, action.Status.ToString());
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

            return new SalariesState(false, null, true, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
        }

        [ReducerMethod]
        public static SalariesState SetEmploymentYearActionReducer(SalariesState state, SetEmploymentYearAction action)
        {
            if (state.SearchRequest is null)
            {
                return InitializeSearchRequest(state, SalarySearchContext.Year, ((int)action.Year).ToString());
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

            return new SalariesState(false, null, true, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
        }

        [ReducerMethod]
        public static SalariesState SetSalarySearchBaseRangeActionReducer(SalariesState state, SetSalarySearchBaseRangeAction action)
        {
            if (state.SearchRequest is null)
            {
                return InitializeSearchRequest(state, SalarySearchContext.BasePayRange, ((int)action.Range).ToString());
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

            return new SalariesState(false, null, true, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
        }

        [ReducerMethod]
        public static SalariesState SetSalarySearchTotalRangeActionReducer(SalariesState state, SetSalarySearchTotalRangeAction action)
        {
            if (state.SearchRequest is null)
            {
                return InitializeSearchRequest(state, SalarySearchContext.TotalPayRange, ((int)action.Range).ToString());
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

            return new SalariesState(false, null, true, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
        }

        [ReducerMethod]
        public static SalariesState SetSalarySortFieldActionReducer(SalariesState state, SetSalarySortFieldAction action)
        {
            var sortField = action.SortField.ToString();

            if (state.SearchRequest is null)
            {
                return InitializeSearchRequest(state, SalarySearchContext.SortField, sortField);
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

            return new SalariesState(false, null, true, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
        }

        [ReducerMethod]
        public static SalariesState SetSalarySortByActionReducer(SalariesState state, SetSalarySortByAction action)
        {
            var sortOption = action.Option.ToString();

            if (state.SearchRequest is null)
            {
                return InitializeSearchRequest(state, SalarySearchContext.SortBy, sortOption);
            }

            var updatedSearchRequest = new EmployeeSalarySearchRequestDto(
                name: state.SearchRequest.Name,
                jobTitle: state.SearchRequest.JobTitle,
                agency: state.SearchRequest.Agency,
                status: state.SearchRequest.Status,
                sortBy: sortOption,
                year: state.SearchRequest.Year,
                sortField: state.SearchRequest.SortField,
                basePayRange: state.SearchRequest.BasePayRange,
                totalPayRange: state.SearchRequest.TotalPayRange);

            return new SalariesState(false, null, true, state.SalaryResults, state.SalaryDetail, updatedSearchRequest);
        }

        private static SalariesState InitializeSearchRequest(SalariesState state, SalarySearchContext context, string? value)
        {
            var searchRequest = context switch
            {
                SalarySearchContext.Name => new EmployeeSalarySearchRequestDto(name: value),
                SalarySearchContext.JobTitle => new EmployeeSalarySearchRequestDto(jobTitle: value),
                SalarySearchContext.Agency => new EmployeeSalarySearchRequestDto(agency: value),
                SalarySearchContext.Status => new EmployeeSalarySearchRequestDto(status: value),
                SalarySearchContext.Year => new EmployeeSalarySearchRequestDto(year: int.TryParse(value, out var parsedValue) ? parsedValue : (int)EmploymentYear.AllYears),
                SalarySearchContext.BasePayRange => new EmployeeSalarySearchRequestDto(basePayRange: int.TryParse(value, out var parsedValue) ? parsedValue : (int)SalarySearchRange.AllSalaries),
                SalarySearchContext.TotalPayRange => new EmployeeSalarySearchRequestDto(totalPayRange: int.TryParse(value, out var parsedValue) ? parsedValue : (int)SalarySearchRange.AllSalaries),
                SalarySearchContext.SortField => new EmployeeSalarySearchRequestDto(sortField: value),
                SalarySearchContext.SortBy => new EmployeeSalarySearchRequestDto(sortBy: value),
                _ => new EmployeeSalarySearchRequestDto()
            };

            return new SalariesState(false, null, true, state.SalaryResults, state.SalaryDetail, searchRequest);
        }
    }
}
