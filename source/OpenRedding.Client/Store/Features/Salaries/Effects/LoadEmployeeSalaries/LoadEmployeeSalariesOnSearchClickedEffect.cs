namespace OpenRedding.Client.Store.Features.Salaries.Effects.LoadEmployeeSalaries
{
    using System;
    using System.Threading.Tasks;
    using Fluxor;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Client.Store;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;

    public class LoadEmployeeSalariesOnSearchClickedEffect : Effect<LoadEmployeesOnSearchClickedAction>
    {
        private readonly OpenReddingApiService _apiService;
        private readonly ILogger<LoadEmployeeSalariesOnSearchClickedEffect> _logger;
        private readonly IState<OpenReddingAppState> _state;

        public LoadEmployeeSalariesOnSearchClickedEffect(OpenReddingApiService apiService, ILogger<LoadEmployeeSalariesOnSearchClickedEffect> logger, IState<OpenReddingAppState> state) =>
            (_apiService, _state, _logger) = (apiService, state, logger);

        protected override async Task HandleAsync(LoadEmployeesOnSearchClickedAction action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Loading employee salaries from search button...");
                var employeeSalaries = await _apiService.GetEmployeesSalariesAsync(_state.Value.SearchRequest);

                _logger.LogInformation("Employee salaries load was successful");
                dispatcher.Dispatch(new LoadEmployeeSalariesSuccessAction(employeeSalaries));
            }
            catch (Exception e)
            {
                _logger.LogError($"Employee salaries failed to load, reason: {e.Message}");
                dispatcher.Dispatch(new LoadEmployeeSalariesFailureAction());
            }
        }
    }
}
