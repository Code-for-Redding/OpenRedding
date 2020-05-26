namespace OpenRedding.Core.Salaries.Commands.DownloadSalaries
{
	using MediatR;
    using OpenRedding.Domain.Salaries.Dtos;

    public class DownloadSalariesCommand : IRequest<byte[]>
    {
        public DownloadSalariesCommand(EmployeeSalarySearchRequestDto searchRequest) =>
            SearchRequest = searchRequest;

        public EmployeeSalarySearchRequestDto SearchRequest { get; }
    }
}
