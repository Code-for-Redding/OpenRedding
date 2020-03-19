namespace OpenRedding.Identity.Areas.Identity.Pages
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
#pragma warning disable SA1649 // File name should match first type name
    public class ErrorModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly string? _requestId;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
            _requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(_requestId))
            {
                _logger.LogWarning("Error page rendered, check for user activity");
            }
            else
            {
                _logger.LogWarning($"Error page rendered for request ID {_requestId}");
            }
        }
    }
}
