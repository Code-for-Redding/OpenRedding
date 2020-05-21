namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaryDetail
{
    public class LoadEmployeeSalaryDetailFromLink
    {
        public LoadEmployeeSalaryDetailFromLink(string link) =>
            Link = link;

        public string Link { get; }
    }
}
