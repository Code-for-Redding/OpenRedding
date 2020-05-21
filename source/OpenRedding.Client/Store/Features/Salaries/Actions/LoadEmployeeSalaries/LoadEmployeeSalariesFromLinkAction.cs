namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries
{
    public class LoadEmployeeSalariesFromLinkAction
    {
        public LoadEmployeeSalariesFromLinkAction(string link) =>
            Link = link;

        public string Link { get; }
    }
}
