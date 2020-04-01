namespace OpenRedding.Identity.Accounts.Commands.RegisterUser
{
    using System;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using OpenRedding.Identity.Models;

    public class RegisterUserCommand : IRequest<RegisteredAccountDto>
    {
        public RegisterUserCommand(RegisterUserAccountViewModel request, ModelStateDictionary modelState, IUrlHelper urlHelper, HttpRequest httpRequest, Uri returnUrl) =>
            (Request, ModelState, UrlHelper, HttpRequest, ReturnUrl) = (request, modelState, urlHelper, httpRequest, returnUrl);

        public RegisterUserAccountViewModel Request { get; }

        public ModelStateDictionary ModelState { get; }

        public IUrlHelper UrlHelper { get; }

        public HttpRequest HttpRequest { get; }

        public Uri ReturnUrl { get; }
    }
}
