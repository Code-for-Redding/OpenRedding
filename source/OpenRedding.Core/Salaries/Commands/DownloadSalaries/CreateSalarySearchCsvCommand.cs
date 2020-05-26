namespace OpenRedding.Core.Salaries.Commands.DownloadSalaries
{
    using System.IO;
    using MediatR;
    using OpenRedding.Domain.Salaries.Dtos;

    public class CreateSalarySearchCsvCommand : IRequest<FileInfo>
    {
        public CreateSalarySearchCsvCommand(EmployeeSalarySearchRequestDto searchRequest) =>
            SearchRequest = searchRequest;

        public EmployeeSalarySearchRequestDto SearchRequest { get; }
    }
}
