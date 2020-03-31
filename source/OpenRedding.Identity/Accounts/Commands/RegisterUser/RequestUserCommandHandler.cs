namespace OpenRedding.Identity.Accounts.Commands.RegisterUser
{
    using System;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading;
    using System.Threading.Tasks;
    using IdentityServer4.Services;
    using IdentityServer4.Stores;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Identity.Models;
    using OpenRedding.Infrastructure.Identity;
    using OpenRedding.Shared.Validation;

    public class RequestUserCommandHandler : IRequestHandler<RegisterUserCommand, ConfirmedRegisteredAccountDto>
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly ILogger<RequestUserCommandHandler> _logger;
        private readonly UserManager<OpenReddingUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public RequestUserCommandHandler(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            ILogger<RequestUserCommandHandler> logger,
            UserManager<OpenReddingUser> userManager,
            IConfiguration configuration,
            IEmailSender emailSender)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public async Task<ConfirmedRegisteredAccountDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            Validate.NotNull(request, nameof(request));
            Validate.NotNull(request.Request, nameof(request.Request));

            var context = await _interaction.GetAuthorizationContextAsync(request.ReturnUrl);

            var user = new OpenReddingUser { UserName = request.Request.Email, Email = request.Request.Email };
            var result = await _userManager.CreateAsync(user, request.Request.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation($"User {request.Request.Email} has successfully been created");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = request.UrlHelper.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code },
                    protocol: request.HttpRequest.Scheme);

                await _emailSender.SendEmailAsync(request.Request.Email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            }

            foreach (var error in result.Errors)
            {
                request.ModelState.AddModelError(string.Empty, error.Description);
            }

            return new ConfirmedRegisteredAccountDto(_userManager.Options.SignIn.RequireConfirmedAccount, user);
        }
    }
}
