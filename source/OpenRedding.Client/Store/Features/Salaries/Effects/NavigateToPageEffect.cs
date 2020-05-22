namespace OpenRedding.Client.Store.Features.Salaries.Effects
{
	using System.Threading.Tasks;
	using Fluxor;
    using Microsoft.AspNetCore.Components;
    using OpenRedding.Client.Store.Features.Salaries.Actions.Navigation;

    public class NavigateToPageEffect : Effect<NavigateToPageAction>
    {
        private readonly NavigationManager _navigation;

        public NavigateToPageEffect(NavigationManager navigation) =>
            _navigation = navigation;

        protected override Task HandleAsync(NavigateToPageAction action, IDispatcher dispatcher)
        {
            _navigation.NavigateTo(action.Page);

            if (action.DispatchSuccess)
            {
                dispatcher.Dispatch(new NavigateToPageSuccessAction());
            }

            return Task.CompletedTask;
        }
    }
}
