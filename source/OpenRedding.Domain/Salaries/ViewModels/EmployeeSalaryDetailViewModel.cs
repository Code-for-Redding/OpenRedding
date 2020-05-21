namespace OpenRedding.Domain.Salaries.ViewModels
{
    using Dtos;

    public class EmployeeSalaryDetailViewModel
    {
        public EmployeeSalaryDetailViewModel(EmployeeSalaryDetailDto employee) =>
            Employee = employee;

        public EmployeeSalaryDetailDto Employee { get; }
    }
}
