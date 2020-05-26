namespace OpenRedding.Core.Salaries.Commands.DownloadSalaries
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using OpenRedding.Shared;

    public class DeleteTemporaryCsvDownloadCommandHandler : IRequestHandler<DeleteTemporaryCsvDownloadCommand>
    {
        public Task<Unit> Handle(DeleteTemporaryCsvDownloadCommand request, CancellationToken cancellationToken)
        {
            ArgumentValidation.CheckNotNull(request, nameof(request));

            // Check for directory existence before deleting
            var directory = $"{Directory.GetCurrentDirectory()}\\Temp";
            if (!Directory.Exists(directory))
            {
                return Task.FromResult(Unit.Value);
            }

            // Delete the resources
            File.Delete(request.FileName);

            // If there are any left over files, delete them all
            var files = Directory.GetFiles(directory);
            if (files.Length > 0)
            {
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }

            Directory.Delete(directory);

            return Task.FromResult(Unit.Value);
        }
    }
}
