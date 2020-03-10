namespace OpenRedding.Identity.Accounts.Commands.LoginUser
{
    using FluentValidation;

    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(r => r.LoginRequestDto)
                .NotNull()
                .WithMessage("Login request cannot be null");

            RuleFor(r => r.LoginRequestDto.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email address must be provided");

            RuleFor(r => r.LoginRequestDto.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password must be provided");

            RuleFor(r => r.Context)
                .NotNull()
                .WithMessage("Login context must be provided");
        }
    }
}