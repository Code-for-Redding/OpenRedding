namespace OpenRedding.Client.Store.Features.Salaries.Effects.LoadEmployeeSalaryDetail
{
    using System;
    using System.Threading.Tasks;
    using Fluxor;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaryDetail;

    public class LoadEmployeeSalaryDetailFromLinkEffect : Effect<LoadEmployeeSalaryDetailFromLinkAction>
    {
		private readonly OpenReddingApiService _apiService;
		private readonly ILogger<LoadEmployeeSalaryDetailFromLinkEffect> _logger;

		public LoadEmployeeSalaryDetailFromLinkEffect(OpenReddingApiService apiService, ILogger<LoadEmployeeSalaryDetailFromLinkEffect> logger) =>
			(_apiService, _logger) = (apiService, logger);

		protected override async Task HandleAsync(LoadEmployeeSalaryDetailFromLinkAction action, IDispatcher dispatcher)
		{
			try
			{
				var employeeSalaries = await _apiService.GetEmployeeSalaryDetailFromLink(action.Link);

				_logger.LogInformation("Employee salaries load was successful");
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
