namespace OpenRedding.Identity.Accounts.Commands.RegisterUser
{
    using MediatR;
    using OpenRedding.Domain.Accounts.ViewModels;
    using OpenRedding.Identity.ViewModels;

    public class RegisterUserCommand : IRequest<RegisteredUserViewModel>
    {
        public RegisterUserCommand(RegisterUserAccountViewModel request) =>
            Request = request;

        public RegisterUserAccountViewModel Request { get; set; }
    }
}
