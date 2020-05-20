namespace OpenRedding.Client.Store.Features.Salaries.Effects.SetSearchRequest
{
    using System;
    using System.Threading.Tasks;
    using Fluxor;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;
    using OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest;

    public class SetSalarySearchTotalPayRangeEffect : Effect<SetSalarySearchTotalRangeAction>
    {
		private readonly OpenReddingApiService _apiService;
		private readonly ILogger<SetSalarySearchTotalPayRangeEffect> _logger;
		private readonly IState<SalariesState> _state;

		public SetSalarySearchTotalPayRangeEffect(OpenReddingApiService apiService, ILogger<SetSalarySearchTotalPayRangeEffect> logger, IState<SalariesState> state) =>
			(_apiService, _logger, _state) = (apiService, logger, state);

		protected override async Task HandleAsync(SetSalarySearchTotalRangeAction action, IDispatcher dispatcher)
		{
			try
			{
				_logger.LogInformation($"Loading employee salaries for total salary range {action.Range}");
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
