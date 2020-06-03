namespace OpenRedding.Client.Store.Features.Salaries
{
    using System.Collections.Generic;
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.ViewModels;

    public class OpenReddingAppState
    {
        public OpenReddingAppState(
            bool isLoading,
            bool isTableRefresh,
            OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>? salaryResults,
            EmployeeSalaryDetailViewModel? salaryDetail,
            EmployeeSalarySearchRequestDto? searchRequest)
        {
            IsLoading = isLoading;
            IsTableRefresh = isTableRefresh;
            SalaryResults = salaryResults;
            SalaryDetail = salaryDetail;
            SearchRequest = searchRequest;
        }

        public bool IsLoading { get; }

        public bool IsTableRefresh { get; }

        public OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>? SalaryResults { get; }

        public EmployeeSalaryDetailViewModel? SalaryDetail { get; }

        public EmployeeSalarySearchRequestDto? SearchRequest { get; }
    }
}
