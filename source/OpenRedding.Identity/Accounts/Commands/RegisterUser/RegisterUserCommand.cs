namespace OpenRedding.Identity.Accounts.Commands.RegisterUser
{
    using MediatR;
    using OpenRedding.Domain.Accounts.Dtos;
    using OpenRedding.Domain.Accounts.ViewModels;

    public class RegisterUserCommand : IRequest<RegisteredUserViewModel>
    {
        public RegisterUserCommand(RegisterUserAccountDto request) =>
            Request = request;

        public RegisterUserAccountDto Request { get; set; }
    }
}
