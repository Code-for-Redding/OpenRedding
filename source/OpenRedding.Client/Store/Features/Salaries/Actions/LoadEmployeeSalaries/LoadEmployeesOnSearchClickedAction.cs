namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries
{
    public class LoadEmployeesOnSearchClickedAction
    {
        public LoadEmployeesOnSearchClickedAction(string name, string jobTitle) =>
            (Name, JobTitle) = (name, jobTitle);

        public string Name { get; }

        public string JobTitle { get; }
    }
}
