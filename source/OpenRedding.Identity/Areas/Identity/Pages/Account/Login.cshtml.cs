namespace OpenRedding.Identity.Areas.Identity.Pages.Account
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Identity.ViewModels;
    using OpenRedding.Infrastructure.Identity;

    [AllowAnonymous]
    public class Login : PageModel
    {
        private readonly UserManager<OpenReddingUser> _userManager;
        private readonly SignInManager<OpenReddingUser> _signInManager;
        private readonly ILogger<Login> _logger;

        public Login(
            SignInManager<OpenReddingUser> signInManager,
            UserManager<OpenReddingUser> userManager,
            ILogger<Login> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            LoginModel = new LoginViewModel();
        }

        public string? ReturnUrl { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        [BindProperty]
        public LoginViewModel LoginModel { get; set; }

#pragma warning disable CA1054 // Uri parameters should not be strings

        public void OnGet(string? returnUrl = null)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

#pragma warning disable CA1054 // Uri parameters should not be strings

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            returnUrl ??= Url.Content("~/");

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(LoginModel.Email, LoginModel.Password, LoginModel.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User on request {Activity.Current?.Id ?? HttpContext.TraceIdentifier} has successfully signed in.");
                return LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning($"User on request {Activity.Current?.Id ?? HttpContext.TraceIdentifier} has attempted to sign in whiled account has been locked.");
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"The login attempt failed, please verify your email address and password before trying again.");
                return Page();
            }
        }
    }
}
