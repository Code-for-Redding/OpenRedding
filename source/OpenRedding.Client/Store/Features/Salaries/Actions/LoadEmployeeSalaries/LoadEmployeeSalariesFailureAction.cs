namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries
{
    using OpenRedding.Client.Store.Features.Shared.Actions;

    public class LoadEmployeeSalariesFailureAction : FailureAction
    {
        public LoadEmployeeSalariesFailureAction(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}
