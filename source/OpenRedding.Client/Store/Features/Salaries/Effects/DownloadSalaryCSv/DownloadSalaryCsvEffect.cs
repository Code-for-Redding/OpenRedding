namespace OpenRedding.Client.Store.Features.Salaries.Effects.DownloadSalaryCSv
{
    using System;
    using System.Threading.Tasks;
    using Fluxor;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;
    using OpenRedding.Client.Store.Features.Salaries.Actions.DownloadSalaryCsv;

    public class DownloadSalaryCsvEffect : Effect<DownloadSalaryCsvAction>
    {
        private readonly ILogger<DownloadSalaryCsvAction> _logger;
        private readonly OpenReddingApiService _apiService;
        private readonly IState<SalariesState> _state;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigation;

        public DownloadSalaryCsvEffect(
            ILogger<DownloadSalaryCsvAction> logger,
            OpenReddingApiService apiService,
            IState<SalariesState> state,
            IJSRuntime jsRuntime,
            NavigationManager navigation)
        {
            _logger = logger;
            _apiService = apiService;
            _state = state;
            _jsRuntime = jsRuntime;
            _navigation = navigation;
        }

        protected override async Task HandleAsync(DownloadSalaryCsvAction action, IDispatcher dispatcher)
        {
            try
            {
                // Trigger the static loading modal
                await _jsRuntime.InvokeVoidAsync("interactWithModal", "#loading-modal", "show");

                // Should return a FileContentResult to the browser for download
                await _apiService.DownloadSalaryReport(_state.Value.SearchRequest);

                // Shut down the loading modal
                await _jsRuntime.InvokeVoidAsync("interactWithModal", "#loading-modal", "hide");
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not download the CSV report, reason: {e.Message}");
                dispatcher.Dispatch(new DownloadSalaryCsvActionFailure());
            }
        }
    }
}
