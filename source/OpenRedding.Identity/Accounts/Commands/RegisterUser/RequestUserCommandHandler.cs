namespace OpenRedding.Identity.Accounts.Commands.RegisterUser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using IdentityServer4.Services;
    using IdentityServer4.Stores;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Domain.Accounts.ViewModels;
    using OpenRedding.Infrastructure.Identity;

    public class RequestUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisteredUserViewModel>
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly ILogger<RequestUserCommandHandler> _logger;
        private readonly UserManager<OpenReddingUser> _userManager;
        private readonly IConfiguration _configuration;

        public RequestUserCommandHandler(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            ILogger<RequestUserCommandHandler> logger,
            UserManager<OpenReddingUser> userManager,
            IConfiguration configuration)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<RegisteredUserViewModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));

            throw new NotImplementedException();
        }
    }
}
