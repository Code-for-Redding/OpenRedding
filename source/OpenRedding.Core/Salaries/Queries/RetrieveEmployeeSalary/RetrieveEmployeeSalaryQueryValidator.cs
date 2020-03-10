namespace OpenRedding.Core.Salaries.Queries.RetrieveEmployeeSalary
{
    using FluentValidation;

    public class RetrieveEmployeeSalaryQueryValidator : AbstractValidator<RetrieveEmployeeSalaryQuery>
    {
        public RetrieveEmployeeSalaryQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Must supply the employee ID to retrieve");
        }
    }
}