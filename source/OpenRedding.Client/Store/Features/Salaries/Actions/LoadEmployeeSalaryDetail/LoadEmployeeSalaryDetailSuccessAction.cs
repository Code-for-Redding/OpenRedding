namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaryDetail
{
    using OpenRedding.Domain.Salaries.ViewModels;

    public class LoadEmployeeSalaryDetailSuccessAction
    {
        public LoadEmployeeSalaryDetailSuccessAction(EmployeeSalaryDetailViewModel salaryDetail) =>
            SalaryDetail = salaryDetail;

        public EmployeeSalaryDetailViewModel SalaryDetail { get; }
    }
}
