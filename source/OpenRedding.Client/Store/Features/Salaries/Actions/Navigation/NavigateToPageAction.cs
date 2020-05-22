namespace OpenRedding.Client.Store.Features.Salaries.Actions.Navigation
{
    public class NavigateToPageAction
    {
        public NavigateToPageAction(string page, bool isLoading, bool dispatchSuccess) =>
            (Page, IsLoading, DispatchSuccess) = (page, isLoading, dispatchSuccess);

        public string Page { get; }

        public bool IsLoading { get; }

        public bool DispatchSuccess { get; }
    }
}
