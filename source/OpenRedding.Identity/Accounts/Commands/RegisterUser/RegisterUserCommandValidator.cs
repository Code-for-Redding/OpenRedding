namespace OpenRedding.Identity.Accounts.Commands.RegisterUser
{
    using FluentValidation;

    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(r => r.Request)
                .NotNull()
                .WithMessage("Must provide the request context");

            RuleFor(r => r.ReturnUrl)
                .NotNull()
                .NotEmpty()
                .WithMessage("Must provide a valid return URL");

            RuleFor(r => r.HttpRequest)
                .NotNull()
                .WithMessage("Must provide the HTTP request context");

            RuleFor(r => r.ModelState)
                .NotNull()
                .WithMessage("Must provide the model state context");
        }
    }
}
