namespace OpenRedding.Identity.Pages.Account
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Infrastructure.Identity;

#pragma warning disable SA1649 // File name should match first type name

    public class RegisterModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(SignInManager<OpenReddingUser> signInManager, UserManager<OpenReddingUser> userManager, ILogger<RegisterModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
        }

        /// <summary>
        /// Handle the registration postback to Identity Server.
        /// </summary>
        public async Task OnPostAsync()
        {
        }
    }
}
