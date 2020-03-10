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
                .WithMessage("Must supply email address");
        }
    }
}