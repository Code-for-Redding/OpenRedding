namespace OpenRedding.Identity.Accounts.Commands.LoginUser
{
    using MediatR;
    using OpenRedding.Domain.Accounts.Dtos;
    using OpenRedding.Identity.Models;

    public class LoginUserCommand : IRequest<LoginViewModel>
    {
        public LoginUserCommand(LoginDto loginRequestDto) =>
            LoginRequestDto = loginRequestDto;

        public LoginDto LoginRequestDto { get; set; }
    }
}