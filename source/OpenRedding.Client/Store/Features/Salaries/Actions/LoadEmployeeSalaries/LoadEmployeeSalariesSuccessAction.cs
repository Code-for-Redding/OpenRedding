namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries
{
	using OpenRedding.Domain.Salaries.ViewModels;

    public class LoadEmployeeSalariesSuccessAction
    {
        public LoadEmployeeSalariesSuccessAction(EmployeeSearchResultViewModelList salaries) =>
            Salaries = salaries;

        public EmployeeSearchResultViewModelList Salaries { get; set; }
    }
}
