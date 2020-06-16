namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaryDetail
{
    public class LoadEmployeeSalaryDetailFromLinkAction
    {
        public LoadEmployeeSalaryDetailFromLinkAction(string link, int id) =>
            (Link, Id) = (link, id);

        public string Link { get; }

        public int Id { get; }
    }
}
