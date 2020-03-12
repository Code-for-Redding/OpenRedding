namespace OpenRedding.Identity.Accounts.Commands.LoginUser
{
    using MediatR;
    using OpenRedding.Domain.Accounts;
    using OpenRedding.Domain.Accounts.Dtos;
    using OpenRedding.Domain.Accounts.ViewModels;

    public class LoginUserCommand : IRequest<LoginViewModel>
    {
        public LoginUserCommand(LoginDto loginRequestDto) =>
            LoginRequestDto = loginRequestDto;

        public LoginDto LoginRequestDto { get; set; }

        public LoginContext Context { get; set; }
    }
}
