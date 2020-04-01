namespace OpenRedding.Identity.Accounts.Commands.LogoutUser
{
	using FluentValidation;

	public class LogoutUserCommandValidator : AbstractValidator<LogoutUserCommand>
    {
		public LogoutUserCommandValidator()
		{
			RuleFor(r => r.LogoutId)
				.NotNull()
				.NotEmpty()
				.WithMessage("Must provide a valid logout ID");

			RuleFor(r => r.ReturnUrl)
				.NotNull()
				.NotEmpty()
				.WithMessage("Must provide a valid return URL");

			RuleFor(r => r.HttpContext)
				.NotNull()
				.NotEmpty()
				.WithMessage("Must provide a valid HTTP context");
		}
    }
}
