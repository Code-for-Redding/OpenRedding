namespace OpenRedding.Identity.Areas.Identity.Pages.Account
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Infrastructure.Identity;
    using OpenRedding.Shared.Validation;

    [AllowAnonymous]
    public class Logout : PageModel
    {
        private readonly SignInManager<OpenReddingUser> _signInManager;
        private readonly ILogger<Logout> _logger;

        public Logout(SignInManager<OpenReddingUser> signInManager, ILogger<Logout> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public string? ReturnUrl { get; set; }

        public void OnGet(Uri returnUrl)
        {
            Validate.NotNull(returnUrl, nameof(returnUrl));

            ReturnUrl = string.IsNullOrWhiteSpace(returnUrl.OriginalString) ? Url.Content("~/") : returnUrl.OriginalString;
        }

        public async Task<IActionResult> OnPostAsync(Uri returnUrl)
        {
            Validate.NotNull(returnUrl, nameof(returnUrl));

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (string.IsNullOrWhiteSpace(returnUrl.OriginalString))
            {
                return RedirectToPage("/Login");
            }
            else
            {
                return Redirect(returnUrl.OriginalString);
            }
        }
    }
}
