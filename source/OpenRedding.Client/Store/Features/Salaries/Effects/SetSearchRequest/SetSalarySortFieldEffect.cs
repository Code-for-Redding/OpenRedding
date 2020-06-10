namespace OpenRedding.Client.Store.Features.Salaries.Effects.SetSearchRequest
{
    using System;
    using System.Threading.Tasks;
    using Fluxor;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Client.Store;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;
    using OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest;

    public class SetSalarySortFieldEffect : Effect<SetSalarySortFieldAction>
    {
        private readonly OpenReddingApiService _apiService;
        private readonly ILogger<SetSalarySortFieldEffect> _logger;
        private readonly IState<OpenReddingAppState> _state;

        public SetSalarySortFieldEffect(OpenReddingApiService apiService, ILogger<SetSalarySortFieldEffect> logger, IState<OpenReddingAppState> state) =>
            (_apiService, _logger, _state) = (apiService, logger, state);

        protected override async Task HandleAsync(SetSalarySortFieldAction action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation($"Sorting salaries by field {action.SortField}");
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
