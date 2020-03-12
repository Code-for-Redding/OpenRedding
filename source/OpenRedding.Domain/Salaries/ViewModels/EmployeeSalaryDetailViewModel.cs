namespace OpenRedding.Domain.Salaries.ViewModels
{
    using Common.ViewModels;
    using Dtos;

    public class EmployeeSalaryDetailViewModel : OpenReddingViewModel
    {
        public EmployeeSalaryDetailViewModel(EmployeeSalaryDetailDto employee) => Employee = employee;

        public EmployeeSalaryDetailDto Employee { get; }
    }
}
