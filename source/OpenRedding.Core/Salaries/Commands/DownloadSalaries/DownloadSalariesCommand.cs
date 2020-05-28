namespace OpenRedding.Core.Salaries.Commands.DownloadSalaries
{
	using MediatR;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Domain.Salaries.Dtos;

    public class DownloadSalariesCommand : IRequest<OpenReddingLink>
    {
        public DownloadSalariesCommand(EmployeeSalarySearchRequestDto searchRequest) =>
            SearchRequest = searchRequest;

        public EmployeeSalarySearchRequestDto SearchRequest { get; }
    }
}
