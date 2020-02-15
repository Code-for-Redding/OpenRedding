namespace OpenRedding.Core.Salaries.Queries.RetrieveEmployeeSalary
{
    using Common;
    using Domain.Salaries.ViewModels;

    public class RetrieveEmployeeSalaryQuery : OpenReddingRequest<EmployeeSalaryDetailViewModel>
    {
        public RetrieveEmployeeSalaryQuery(int id) => Id = id;

        public int Id { get; }
    }
}