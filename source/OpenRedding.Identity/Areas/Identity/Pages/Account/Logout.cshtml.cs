namespace OpenRedding.Identity.Areas.Identity.Pages.Account
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Infrastructure.Identity;

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

        public void OnGet()
        {
        }

#pragma warning disable CA1054 // Uri parameters should not be strings

        public async Task<IActionResult> OnPost(string? returnUrl = null)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
