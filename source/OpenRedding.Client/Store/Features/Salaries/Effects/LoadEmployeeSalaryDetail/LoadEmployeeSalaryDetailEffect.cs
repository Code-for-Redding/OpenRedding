namespace OpenRedding.Client.Store.Features.Salaries.Effects.LoadEmployeeSalaryDetail
{
    using System;
    using System.Threading.Tasks;
    using Fluxor;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaryDetail;

    public class LoadEmployeeSalaryDetailEffect : Effect<LoadEmployeeSalaryDetailAction>
    {
        private readonly OpenReddingApiService _apiService;
        private readonly ILogger<LoadEmployeeSalaryDetailEffect> _logger;
        private readonly NavigationManager _navigation;

        public LoadEmployeeSalaryDetailEffect(OpenReddingApiService apiService, ILogger<LoadEmployeeSalaryDetailEffect> logger, NavigationManager navigation) =>
            (_apiService, _navigation, _logger) = (apiService, navigation, logger);

        protected override async Task HandleAsync(LoadEmployeeSalaryDetailAction action, IDispatcher dispatcher)
        {
            try
            {
                _navigation.NavigateTo($"salaries/detail/{action.Id}");

                _logger.LogInformation("Loading employee detail from ID...");
                var employeeSalaries = await _apiService.GetEmployeeSalaryDetailFromId(action.Id);

                _logger.LogInformation($"Employee salary {action.Id} load was successful");
                dispatcher.Dispatch(new LoadEmployeeSalaryDetailSuccessAction(employeeSalaries));
            }
            catch (Exception e)
            {
                _logger.LogError($"Employee salaries failed to load, reason: ${e.Message}");
                dispatcher.Dispatch(new LoadEmployeeSalaryDetailFailureAction());
            }
        }
    }
}
