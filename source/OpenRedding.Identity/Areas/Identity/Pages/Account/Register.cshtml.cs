namespace OpenRedding.Identity.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Identity.Accounts.Commands.RegisterUser;
    using OpenRedding.Identity.Models;
    using OpenRedding.Infrastructure.Identity;
    using OpenRedding.Shared.Validation;

    [AllowAnonymous]
    public class Register : PageModel
    {
        private readonly ILogger<Register> _logger;
        private readonly IMediator _mediator;
        private readonly SignInManager<OpenReddingUser> _signInManager;

        public Register(ILogger<Register> logger, IMediator mediator, SignInManager<OpenReddingUser> signInManager)
        {
            _logger = logger;
            _mediator = mediator;
            _signInManager = signInManager;
            RegistrationModel = new RegisterUserAccountViewModel();
            RouteParameters = new Dictionary<string, string?>();
        }

        [BindProperty]
        public RegisterUserAccountViewModel RegistrationModel { get; set; }

        public string? ReturnUrl { get; set; }

        public IDictionary<string, string?> RouteParameters { get; set; }

        public void OnGet(Uri returnUrl)
        {
            Validate.NotNull(returnUrl, nameof(returnUrl));
            ReturnUrl = returnUrl.OriginalString;
            RouteParameters.TryAdd("returnUrl", ReturnUrl);
        }

        /// <summary>
        /// Handle the registration postback to Identity Server.
        /// </summary>
        /// <param name="returnUrl">Nullable return URL sent from the client.</param>
        /// <returns>User registertration task result.</returns>
        public async Task<IActionResult> OnPostAsync(Uri returnUrl)
        {
            Validate.NotNull(returnUrl, nameof(returnUrl));

            var scrubbedReturnUrl = string.IsNullOrWhiteSpace(returnUrl.OriginalString) ? Url.Content("~/") : returnUrl.OriginalString;
            if (!ModelState.IsValid || RegistrationModel is null)
            {
                _logger.LogWarning("Invalid user registration attempted");

                // Re-render the page with the appropiate error messages
                return Page();
            }

            _logger.LogInformation("Validating user registration request");
            var command = new RegisterUserCommand(RegistrationModel, ModelState, Url, Request, scrubbedReturnUrl);
            var registeredUser = await _mediator.Send(command);
            Validate.NotNull(registeredUser, nameof(registeredUser));

            if (registeredUser.RequireConfirmedAccount)
            {
                return RedirectToPage("RegisterConfirmation", new { email = registeredUser.User.Email });
            }
            else
            {
                await _signInManager.SignInAsync(registeredUser.User, isPersistent: false);
                return LocalRedirect(scrubbedReturnUrl);
            }
        }
    }
}
