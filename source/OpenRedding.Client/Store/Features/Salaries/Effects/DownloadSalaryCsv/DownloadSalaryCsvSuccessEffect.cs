namespace OpenRedding.Client.Store.Features.Salaries.Effects.DownloadSalaryCsv
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Fluxor;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;
    using OpenRedding.Client.Store.Features.Salaries.Actions.DownloadSalaryCsv;

    public class DownloadSalaryCsvSuccessEffect : Effect<DownloadSalaryCsvSuccessAction>
    {
        private readonly ILogger<DownloadSalaryCsvSuccessEffect> _logger;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigation;

        public DownloadSalaryCsvSuccessEffect(
            ILogger<DownloadSalaryCsvSuccessEffect> logger,
            IJSRuntime jsRuntime,
            NavigationManager navigation)
        {
            _logger = logger;
            _jsRuntime = jsRuntime;
            _navigation = navigation;
        }

        protected override async Task HandleAsync(DownloadSalaryCsvSuccessAction action, IDispatcher dispatcher)
        {
            // Shut down the loading modal
            await _jsRuntime.InvokeVoidAsync("interactWithModal", "#loading-modal", "hide");

            // Navigate the user to the CSV download link
            _navigation.NavigateTo(action.CsvLink);
        }
    }
}
