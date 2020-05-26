namespace OpenRedding.Core.Salaries.Commands.DownloadSalaries
{
	using MediatR;

    public class DeleteTemporaryCsvDownloadCommand : IRequest
    {
        public DeleteTemporaryCsvDownloadCommand(string fileName) =>
            FileName = fileName;

        public string FileName { get; }
    }
}
