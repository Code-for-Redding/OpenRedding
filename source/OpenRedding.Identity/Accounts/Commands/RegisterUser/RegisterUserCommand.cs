namespace OpenRedding.Identity.Accounts.Commands.RegisterUser
{
    using MediatR;
    using OpenRedding.Domain.Accounts.ViewModels;
    using OpenRedding.Identity.Models;

    public class RegisterUserCommand : IRequest<RegisteredUserViewModel>
    {
        public RegisterUserCommand(RegisterUserAccountDto request) =>
            Request = request;

        public RegisterUserAccountDto Request { get; set; }
    }
}
