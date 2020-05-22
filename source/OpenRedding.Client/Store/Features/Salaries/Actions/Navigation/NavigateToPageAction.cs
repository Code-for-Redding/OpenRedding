namespace OpenRedding.Client.Store.Features.Salaries.Actions.Navigation
{
    public class NavigateToPageAction
    {
        public NavigateToPageAction(string page, bool dispatchSuccess) =>
            (Page, DispatchSuccess) = (page, dispatchSuccess);

        public string Page { get; }

        public bool DispatchSuccess { get; }
    }
}
