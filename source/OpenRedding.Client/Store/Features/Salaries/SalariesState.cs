namespace OpenRedding.Client.Store.Features.Salaries
{
    using System.Collections.Generic;
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.ViewModels;

    public class SalariesState
    {
        public SalariesState(bool isLoading, bool isTableRefresh, OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>? salaryResults, EmployeeSalaryDetailViewModel? salaryDetail)
        {
            IsLoading = isLoading;
            IsTableRefresh = isTableRefresh;
            SalaryResults = salaryResults;
            SalaryDetail = salaryDetail;
        }

        public bool IsLoading { get; }

        public bool IsTableRefresh { get; set; }

        public OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>? SalaryResults { get; }

        public EmployeeSalaryDetailViewModel? SalaryDetail { get; }
    }
}
