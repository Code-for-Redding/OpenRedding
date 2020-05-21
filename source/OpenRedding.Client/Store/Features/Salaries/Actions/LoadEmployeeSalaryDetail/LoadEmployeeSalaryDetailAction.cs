namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaryDetail
{
    public class LoadEmployeeSalaryDetailAction
    {
        public LoadEmployeeSalaryDetailAction(int employeeId) =>
            EmployeeId = employeeId;

        public int EmployeeId { get; set; }
    }
}
