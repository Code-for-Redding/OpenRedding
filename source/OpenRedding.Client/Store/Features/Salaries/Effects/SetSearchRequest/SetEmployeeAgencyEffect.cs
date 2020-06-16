namespace OpenRedding.Client.Store.Features.Salaries.Effects.SetSearchRequest
{
    using System;
    using System.Threading.Tasks;
    using Fluxor;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;
    using OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest;
    using OpenRedding.Client.Store.State;

    public class SetEmployeeAgencyEffect : Effect<SetEmployeeAgencyAction>
    {
        private readonly OpenReddingApiService _apiService;
        private readonly ILogger<SetEmployeeAgencyEffect> _logger;
        private readonly IState<SalariesState> _state;

        public SetEmployeeAgencyEffect(OpenReddingApiService apiService, ILogger<SetEmployeeAgencyEffect> logger, IState<SalariesState> state) =>
            (_apiService, _logger, _state) = (apiService, logger, state);

        protected override async Task HandleAsync(SetEmployeeAgencyAction action, IDispatcher dispatcher)
        {
            if (action.LoadFromApi)
            {
                try
                {
                    _logger.LogInformation($"Loading employee salaries for agency {action.Agency}");
                    var response = await _apiService.GetEmployeesSalariesAsync(_state.Value.SearchRequest);

                    dispatcher.Dispatch(new LoadEmployeeSalariesSuccessAction(response));
                }
                catch (Exception e)
                {
                    _logger.LogError($"Could not load employee salaries, reason: {e.Message}");
                    dispatcher.Dispatch(new LoadEmployeeSalariesFailureAction(e.Message));
                }
            }
        }
    }
}
