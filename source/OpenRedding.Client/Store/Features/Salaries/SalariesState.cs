namespace OpenRedding.Client.Store.Features.Salaries
{
    using OpenRedding.Domain.Salaries.ViewModels;

    public class SalariesState
    {
        public SalariesState(bool isLoading, bool isTableRefresh, EmployeeSearchResultViewModelList? salaryResults, EmployeeSalaryDetailViewModel? salaryDetail)
        {
            IsLoading = isLoading;
            IsTableRefresh = isTableRefresh;
            SalaryResults = salaryResults;
            SalaryDetail = salaryDetail;
        }

        public bool IsLoading { get; }

        public bool IsTableRefresh { get; set; }

        public EmployeeSearchResultViewModelList? SalaryResults { get; }

        public EmployeeSalaryDetailViewModel? SalaryDetail { get; }
    }
}
