namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaryDetail
{
    public class LoadEmployeeSalaryDetailFromLinkAction
    {
        public LoadEmployeeSalaryDetailFromLinkAction(string link) =>
            Link = link;

        public string Link { get; }
    }
}
