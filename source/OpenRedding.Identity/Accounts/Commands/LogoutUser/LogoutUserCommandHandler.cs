namespace OpenRedding.Identity.Accounts.Commands.LogoutUser
{
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using IdentityServer4.Services;
    using MediatR;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, string>
    {
        private readonly ILogger<LogoutUserCommandHandler> _logger;
        private readonly IIdentityServerInteractionService _interaction;

        public LogoutUserCommandHandler(ILogger<LogoutUserCommandHandler> logger, IIdentityServerInteractionService interaction)
        {
            _logger = logger;
            _interaction = interaction;
        }

        public async Task<string> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.HttpContext.User;
            if (user is null)
            {
                // No user found, fail the logout attempt
                return string.Empty;
            }

            // Delete authentication cookie and reset the identity to an anonymous user
            await request.HttpContext.SignOutAsync();
            await request.HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            request.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            // Get the context information (client name, post logout redirect URI and iframe for federated signout)
            var logoutContext = await _interaction.GetLogoutContextAsync(request.LogoutId);

            return logoutContext.PostLogoutRedirectUri;
        }
    }
}
