namespace OpenRedding.Client.Store.Features.Salaries.Effects
{
	using System;
	using System.Threading.Tasks;
	using Fluxor;
	using Microsoft.Extensions.Logging;
	using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;

	public class LoadEmployeeSalariesEffect : Effect<LoadEmployeeSalariesAction>
	{
		private readonly OpenReddingApiService _apiService;
		private readonly ILogger<LoadEmployeeSalariesEffect> _logger;

		public LoadEmployeeSalariesEffect(OpenReddingApiService apiService, ILogger<LoadEmployeeSalariesEffect> logger) =>
			(_apiService, _logger) = (apiService, logger);

		protected override async Task HandleAsync(LoadEmployeeSalariesAction action, IDispatcher dispatcher)
		{
			try
			{
				var employeeSalaries = await _apiService.GetEmployeesSalariesAsync();

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
