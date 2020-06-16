namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaryDetail
{
    public class LoadEmployeeSalaryDetailAction
    {
        public LoadEmployeeSalaryDetailAction(string id) =>
            Id = id;

        public string Id { get; }
    }
}
