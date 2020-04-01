namespace OpenRedding.Identity.Accounts.Commands.LogoutUser
{
    using System;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class LogoutUserCommand : IRequest<string>
    {
        public LogoutUserCommand(string logoutId, Uri returnUrl, HttpContext httpContext) =>
            (LogoutId, ReturnUrl, HttpContext) = (logoutId, returnUrl, httpContext);

        public string LogoutId { get; }

        public Uri ReturnUrl { get; }

        public HttpContext HttpContext { get; }
    }
}
