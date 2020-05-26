namespace OpenRedding.Core.Salaries.Commands.DownloadSalaries
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using CsvHelper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Core.Data;
    using OpenRedding.Core.Extensions;
    using OpenRedding.Shared;

    public class CreateSalarySearchCsvCommandHandler : IRequestHandler<CreateSalarySearchCsvCommand, FileInfo>
    {
        private readonly IOpenReddingDbContext _context;
        private readonly ILogger<CreateSalarySearchCsvCommandHandler> _logger;

        public CreateSalarySearchCsvCommandHandler(IOpenReddingDbContext context, ILogger<CreateSalarySearchCsvCommandHandler> logger) =>
            (_context, _logger) = (context, logger);

        public async Task<FileInfo> Handle(CreateSalarySearchCsvCommand request, CancellationToken cancellationToken)
        {
            ArgumentValidation.CheckNotNull(request, nameof(request));
            ArgumentValidation.CheckNotNull(request.SearchRequest, nameof(request.SearchRequest));

            // Get a list of the user's current view of the data
            var results = await _context.Employees
                .AsNoTracking()
                .FromSearchRequest(request.SearchRequest)
                .Select(e => e.ToSalaryExportDto())
                .ToListAsync(cancellationToken);

            var tempFolder = $"{Directory.GetCurrentDirectory()}\\Temp";

            if (!Directory.Exists(tempFolder))
            {
                _logger.LogInformation("Create temporary download folder...");
                Directory.CreateDirectory(tempFolder);
            }

            var fileName = $"{tempFolder}\\salaries-{Guid.NewGuid()}.csv";

            using var writer = new StreamWriter(fileName);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(results);

            _logger.LogInformation($"CSV {fileName} created successfully");
            return new FileInfo(fileName);
        }
    }
}
