namespace OpenRedding.Core.Accounts.Commands.ConfirmEmailChange
{
    using FluentValidation;

    public class ConfirmEmailChangeCommandValidator : AbstractValidator<ConfirmEmailChangeCommand>
    {
        public ConfirmEmailChangeCommandValidator()
        {
            RuleFor(r => r.Request)
                .NotNull()
                .WithMessage("Must supply email change data to process request");

            RuleFor(r => r.Request.Code)
                .NotEmpty()
                .NotNull()
                .WithMessage("Must supply the email code to confirm email");

            RuleFor(r => r.Request.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Must supply the email on the request");

            RuleFor(r => r.Request.UserId)
                .NotNull()
                .NotNull()
                .WithMessage("Must supply the user ID");
        }
    }
}
