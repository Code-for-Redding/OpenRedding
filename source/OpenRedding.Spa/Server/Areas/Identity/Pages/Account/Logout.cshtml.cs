namespace OpenRedding.Identity.Areas.Identity.Pages.Account
{
    using System;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Identity.Accounts.Commands.LogoutUser;
    using OpenRedding.Infrastructure.Identity;
    using OpenRedding.Shared.Validation;

    [AllowAnonymous]
    public class Logout : PageModel
    {
        private readonly SignInManager<OpenReddingUser> _signInManager;
        private readonly ILogger<Logout> _logger;
        private readonly IMediator _mediator;

        public Logout(SignInManager<OpenReddingUser> signInManager, ILogger<Logout> logger, IMediator mediator)
        {
            _signInManager = signInManager;
            _logger = logger;
            _mediator = mediator;
        }

        public string? ReturnUrl { get; set; }

        public void OnGet(string logoutId, Uri returnUrl)
        {
            LogoutOrValidateRequest(logoutId, returnUrl);

            ReturnUrl = string.IsNullOrWhiteSpace(returnUrl.OriginalString) ? Url.Content("~/") : returnUrl.OriginalString;
        }

        public async Task<IActionResult> OnPostAsync(string logoutId, Uri returnUrl)
        {
            LogoutOrValidateRequest(logoutId, returnUrl);

            var logoutRequest = new LogoutUserCommand(logoutId, returnUrl, HttpContext);
            var valdiatedReturnUrl = await _mediator.Send(logoutRequest);

            if (string.IsNullOrWhiteSpace(valdiatedReturnUrl))
            {
                return RedirectToPage("/LoggedOut");
            }
            else
            {
                return Redirect(valdiatedReturnUrl);
            }
        }

        private void LogoutOrValidateRequest(string logoutId, Uri returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // User is not authenticated, so redirect to logged out page
                RedirectToPage("/LoggedOut");
            }

            Validate.NotNull(logoutId, nameof(logoutId));
            Validate.NotNull(returnUrl, nameof(returnUrl));
        }
    }
}
