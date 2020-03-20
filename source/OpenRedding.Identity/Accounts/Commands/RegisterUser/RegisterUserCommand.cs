namespace OpenRedding.Identity.Accounts.Commands.RegisterUser
{
    using MediatR;
    using OpenRedding.Identity.ViewModels;

    public class RegisterUserCommand : IRequest<RegisterUserAccountViewModel>
    {
        public RegisterUserCommand(RegisterUserAccountViewModel request) =>
            Request = request;

        public RegisterUserAccountViewModel Request { get; set; }
    }
}
