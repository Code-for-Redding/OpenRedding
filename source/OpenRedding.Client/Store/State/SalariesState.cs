namespace OpenRedding.Client.Store.State
{
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.ViewModels;

    public class SalariesState
    {
        public SalariesState(
            bool isLoading,
            bool isTableRefresh,
            OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>? salaryResults,
            EmployeeSalaryDetailViewModel? salaryDetail,
            EmployeeSalarySearchRequestDto? searchRequest,
            string? errorMessage = null)
        {
            IsLoading = isLoading;
            IsTableRefresh = isTableRefresh;
            SalaryResults = salaryResults;
            SalaryDetail = salaryDetail;
            SearchRequest = searchRequest;
            CurrentErrorMessage = errorMessage;
        }

        public bool IsLoading { get; }

        public bool IsTableRefresh { get; }

        public OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>? SalaryResults { get; }

        public EmployeeSalaryDetailViewModel? SalaryDetail { get; }

        public EmployeeSalarySearchRequestDto? SearchRequest { get; }

        public string? CurrentErrorMessage { get; }

        public bool HasCurrentErrors => !string.IsNullOrWhiteSpace(CurrentErrorMessage);
    }
}
