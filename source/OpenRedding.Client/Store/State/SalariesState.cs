namespace OpenRedding.Client.Store.State
{
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.ViewModels;

    public class SalariesState : RootState
    {
        public SalariesState(bool isLoading, string? errorMessage, bool isTableRefresh, OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>? salaryResults, EmployeeSalaryDetailViewModel? salaryDetail, EmployeeSalarySearchRequestDto? searchRequest)
            : base(isLoading, errorMessage)
        {
            IsTableRefresh = isTableRefresh;
            SalaryResults = salaryResults;
            SalaryDetail = salaryDetail;
            SearchRequest = searchRequest;
        }

        public bool IsTableRefresh { get; }

        public OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>? SalaryResults { get; }

        public EmployeeSalaryDetailViewModel? SalaryDetail { get; }

        public EmployeeSalarySearchRequestDto? SearchRequest { get; }
    }
}
