namespace OpenRedding.Client.Store.Features.Salaries.Effects.DownloadSalaryCsv
{
    using System;
    using System.Threading.Tasks;
    using Fluxor;
    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;
    using OpenRedding.Client.Store;
    using OpenRedding.Client.Store.Features.Salaries.Actions.DownloadSalaryCsv;

    public class DownloadSalaryCsvEffect : Effect<DownloadSalaryCsvAction>
    {
        private readonly ILogger<DownloadSalaryCsvAction> _logger;
        private readonly OpenReddingApiService _apiService;
        private readonly IState<OpenReddingAppState> _state;
        private readonly IJSRuntime _jsRuntime;

        public DownloadSalaryCsvEffect(
            ILogger<DownloadSalaryCsvAction> logger,
            OpenReddingApiService apiService,
            IState<OpenReddingAppState> state,
            IJSRuntime jsRuntime)
        {
            _logger = logger;
            _apiService = apiService;
            _state = state;
            _jsRuntime = jsRuntime;
        }

        protected override async Task HandleAsync(DownloadSalaryCsvAction action, IDispatcher dispatcher)
        {
            try
            {
                // Trigger the static loading modal
                await _jsRuntime.InvokeVoidAsync("interactWithModal", "#loading-modal", "show");

                // Should return a FileContentResult to the browser for download
                var link = await _apiService.GetDownloadCsvLink(_state.Value.SearchRequest);

                // Issue the link in the success action for the client to follow
                dispatcher.Dispatch(new DownloadSalaryCsvSuccessAction(link.Href!));
            }
            catch (Exception e)
            {
                // Shut down the loading modal on error
                await _jsRuntime.InvokeVoidAsync("interactWithModal", "#loading-modal", "hide");
                _logger.LogError($"Could not download the CSV report, reason: {e.Message}");
                dispatcher.Dispatch(new DownloadSalaryCsvFailureAction());
            }
        }
    }
}
