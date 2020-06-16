namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaryDetail
{
    using OpenRedding.Client.Store.Features.Shared.Actions;

    public class LoadEmployeeSalaryDetailFailureAction : FailureAction
    {
        public LoadEmployeeSalaryDetailFailureAction(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}
