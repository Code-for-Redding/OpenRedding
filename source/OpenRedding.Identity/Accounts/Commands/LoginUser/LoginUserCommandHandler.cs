namespace OpenRedding.Identity.Accounts.Commands.LoginUser
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using IdentityServer4.Stores;
    using MediatR;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Domain.Accounts;
    using OpenRedding.Domain.Accounts.ViewModels;
    using OpenRedding.Infrastructure.Identity;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginViewModel>
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly ILogger<LoginUserCommandHandler> _logger;
        private readonly UserManager<OpenReddingUser> _userManager;
        private readonly SignInManager<OpenReddingUser> _signInManager;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public LoginUserCommandHandler(IIdentityServerInteractionService interaction, IClientStore clientStore, ILogger<LoginUserCommandHandler> logger, UserManager<OpenReddingUser> userManager, SignInManager<OpenReddingUser> signInManager, IAuthenticationSchemeProvider schemeProvider, IEventService events)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _schemeProvider = schemeProvider;
            _events = events;
        }

        public async Task<LoginViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var context = await _interaction.GetAuthorizationContextAsync(request.LoginRequestDto.ReturnUrl);
            LoginViewModel loginViewModel;

            switch (request.Context)
            {
                case LoginContext.Initiate:
                    loginViewModel = new LoginViewModel
                    {
                        ReturnUrl = request.LoginRequestDto.ReturnUrl,
                        Email = context?.LoginHint
                    };

                    break;
                case LoginContext.Authorize:
                    loginViewModel = new LoginViewModel();
                    break;
                default:
                    if (context != null)
                    {
                        // If the user cancels, send a result back into IdentityServer as if they
                        // denied the consent (even if this client does not require consent).
                        // This will send back an access denied OIDC error response to the client.
                        await _interaction.GrantConsentAsync(context, ConsentResponse.Denied);
                    }

                    break;
            }

            return loginViewModel;
        }
    }
}