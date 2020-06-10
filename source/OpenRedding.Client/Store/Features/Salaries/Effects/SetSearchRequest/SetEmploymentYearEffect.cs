namespace OpenRedding.Client.Store.Features.Salaries.Effects.SetSearchRequest
{
    using System;
    using System.Threading.Tasks;
    using Fluxor;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Client.Store;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;
    using OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest;

    public class SetEmploymentYearEffect : Effect<SetEmploymentYearAction>
    {
        private readonly OpenReddingApiService _apiService;
        private readonly ILogger<SetEmploymentYearEffect> _logger;
        private readonly IState<OpenReddingAppState> _state;

        public SetEmploymentYearEffect(OpenReddingApiService apiService, ILogger<SetEmploymentYearEffect> logger, IState<OpenReddingAppState> state) =>
            (_apiService, _logger, _state) = (apiService, logger, state);

        protected override async Task HandleAsync(SetEmploymentYearAction action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation($"Loading employee salaries for employment year {action.Year}");
                var response = await _apiService.GetEmployeesSalariesAsync(_state.Value.SearchRequest);

                dispatcher.Dispatch(new LoadEmployeeSalariesSuccessAction(response));
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not load employee salaries, reason: {e.Message}");
                dispatcher.Dispatch(new LoadEmployeeSalariesFailureAction());
            }
        }
    }
}
