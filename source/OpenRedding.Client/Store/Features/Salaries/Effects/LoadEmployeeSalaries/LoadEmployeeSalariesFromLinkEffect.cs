namespace OpenRedding.Client.Store.Features.Salaries.Effects.LoadEmployeeSalaries
{
    using System;
    using System.Threading.Tasks;
    using Fluxor;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Client;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;

    public class LoadEmployeeSalariesFromLinkEffect : Effect<LoadEmployeeSalariesFromLinkAction>
    {
        private readonly OpenReddingApiService _apiService;
        private readonly ILogger<LoadEmployeeSalariesFromLinkEffect> _logger;
        private readonly NavigationManager _navigation;

        public LoadEmployeeSalariesFromLinkEffect(OpenReddingApiService apiService, ILogger<LoadEmployeeSalariesFromLinkEffect> logger, NavigationManager navigation) =>
            (_apiService, _navigation, _logger) = (apiService, navigation, logger);

        protected override async Task HandleAsync(LoadEmployeeSalariesFromLinkAction action, IDispatcher dispatcher)
        {
            try
            {
                _navigation.NavigateTo("salaries");

                _logger.LogInformation("Loading employees from link...");
                var employeeSalaries = await _apiService.GetEmployeesSalariesFromLinkAsync(action.Link);

                _logger.LogInformation("Employee salaries load was successful");
                dispatcher.Dispatch(new LoadEmployeeSalariesSuccessAction(employeeSalaries));
            }
            catch (Exception e)
            {
                _logger.LogError($"Employee salaries failed to load, reason: ${e.Message}");
                dispatcher.Dispatch(new LoadEmployeeSalariesFailureAction());
            }
        }
    }
}
