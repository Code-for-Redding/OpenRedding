namespace OpenRedding.Identity.Pages.Account
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Identity.Accounts.Commands.RegisterUser;
    using OpenRedding.Identity.Models;

#pragma warning disable SA1649 // File name should match first type name

    [AllowAnonymous]
    public class RegisterModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly ILogger<RegisterModel> _logger;
        private readonly IMediator _mediator;

        public RegisterModel(ILogger<RegisterModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            RegistrationModel = new RegisterUserAccountDto();
        }

        [BindProperty]
        public RegisterUserAccountDto RegistrationModel { get; set; }

        public string? ReturnUrl { get; set; }

#pragma warning disable CA1054 // Uri parameters should not be strings

        public void OnGet(string? returnUrl)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            ReturnUrl = returnUrl;
        }

        /// <summary>
        /// Handle the registration postback to Identity Server.
        /// </summary>
        /// <param name="returnUrl">Nullable return URL sent from the client.</param>
        /// <returns>User registertration task result.</returns>
#pragma warning disable CA1054 // Uri parameters should not be strings
        public async Task<IActionResult> OnPostAsync(string? returnUrl)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid user registration attempted");

                // Re-render the page with the appropiate error messages
                return Page();
            }

            _logger.LogInformation("Validating user registration request");
            var command = new RegisterUserCommand(RegistrationModel);
            var response = await _mediator.Send(command);

            return LocalRedirect(returnUrl);
        }
    }
}
