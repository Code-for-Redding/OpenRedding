namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
    public class SetEmployeeJobTitleAction
    {
        public SetEmployeeJobTitleAction(string jobTitle) =>
            JobTitle = jobTitle;

        public string JobTitle { get; }
    }
}
