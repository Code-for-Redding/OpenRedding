namespace OpenRedding.Identity.Controllers
{
    using System;
    using System.Threading.Tasks;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using IdentityServer4.Stores;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Identity.Models;
    using OpenRedding.Infrastructure.Identity;

    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<OpenReddingUser> _userManager;

        public AccountController(IIdentityServerInteractionService interaction, IClientStore clientStore, ILogger<AccountController> logger, UserManager<OpenReddingUser> userManager)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
#pragma warning disable CA1054 // Uri parameters should not be strings
        public async Task<IActionResult> Login(string returnUrl)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context is null)
            {
                return BadRequest("There was an issue processing the request, please try again later.");
            }

            if (context?.IdP != null)
            {
                throw new NotImplementedException("External login is not implemented!");
            }

            var loginViewModel = await BuildLoginViewModelAsync(returnUrl, context!);
            ViewData["ReturnUrl"] = returnUrl;

            return View(loginViewModel);
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl, AuthorizationRequest context)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                throw new ArgumentNullException(nameof(returnUrl), "Return URL must be provided");
            }

            var allowLocal = true;
            if (context?.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;
                }
            }

            return new LoginViewModel
            {
                ReturnUrl = returnUrl,
                Email = context?.LoginHint,
            };
        }
    }
}