namespace OpenRedding.Core.Salaries.Commands.DownloadSalaries
{
    using FluentValidation;

    public class DownloadSalariesCommandValidator : AbstractValidator<DownloadSalariesCommand>
    {
        public DownloadSalariesCommandValidator()
        {
            RuleFor(c => c.SearchRequest)
                .NotNull();
        }
    }
}
