namespace OpenRedding.Client.Store.Features.Salaries.Effects.LoadEmployeeSalaries
{
	using System;
	using System.Threading.Tasks;
	using Fluxor;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Logging;
	using OpenRedding.Client;
	using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries;

	public class LoadEmployeeSalariesEffect : Effect<LoadEmployeeSalariesAction>
	{
		private readonly OpenReddingApiService _apiService;
		private readonly ILogger<LoadEmployeeSalariesEffect> _logger;
		private readonly NavigationManager _navigation;

		public LoadEmployeeSalariesEffect(OpenReddingApiService apiService, ILogger<LoadEmployeeSalariesEffect> logger, NavigationManager navigation) =>
			(_apiService, _navigation, _logger) = (apiService, navigation, logger);

		protected override async Task HandleAsync(LoadEmployeeSalariesAction action, IDispatcher dispatcher)
		{
			try
			{
				_navigation.NavigateTo("salaries");

				_logger.LogInformation("Loading employee salaries...");
				var employeeSalaries = await _apiService.GetEmployeesSalariesAsync(action.SearchRequest);

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
