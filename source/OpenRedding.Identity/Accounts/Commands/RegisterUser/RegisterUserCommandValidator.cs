namespace OpenRedding.Identity.Accounts.Commands.RegisterUser
{
    using FluentValidation;

    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        // Must supply one uppercase alpha, one lowercase alpha, one number, and one special character
        private const string PasswordRequirement = "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d!$%@#£€*?&]{8,}$";

        public RegisterUserCommandValidator()
        {
            RuleFor(r => r.Request)
                .NotNull()
                .WithMessage("Must provide user registration information");

            RuleFor(r => r.Request.EmailAddress)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email address cannot empty or null, and must be a valid address");

            RuleFor(r => r.Request.ConfirmedEmailAddress)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .Equal(r => r.Request.EmailAddress)
                .WithMessage("Confirmed email address does not match the previous address");

            RuleFor(r => r.Request.Password)
                .NotNull()
                .NotEmpty()
                .Matches(PasswordRequirement)
                .WithMessage("Must supply a valid password");

            RuleFor(r => r.Request.ConfirmedPassword)
                .NotNull()
                .NotEmpty()
                .Matches(PasswordRequirement)
                .Equal(r => r.Request.Password)
                .WithMessage("Password is invalid or does not match the previous");
        }
    }
}
