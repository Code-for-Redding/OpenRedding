namespace OpenRedding.Domain.Salaries.Queries
{
	using OpenRedding.Domain.Common.Requests;
	using OpenRedding.Domain.Salaries.ViewModels;

	public class RetrieveEmployeeSalaryQuery : OpenReddingRequest<EmployeeSalaryDetailViewModel>
    {
        public RetrieveEmployeeSalaryQuery(int id) => Id = id;

        public int Id { get; }
    }
}
