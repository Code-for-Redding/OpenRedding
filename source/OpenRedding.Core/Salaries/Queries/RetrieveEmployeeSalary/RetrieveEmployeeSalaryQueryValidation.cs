namespace OpenRedding.Core.Salaries.Queries.RetrieveEmployeeSalary
{
    using FluentValidation;

    public class RetrieveEmployeeSalaryQueryValidation : AbstractValidator<RetrieveEmployeeSalaryQuery>
    {
        public RetrieveEmployeeSalaryQueryValidation()
        {
            RuleFor(q => q.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Must supply the employee ID to retrieve");
        }
    }
}