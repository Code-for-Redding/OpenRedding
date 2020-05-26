namespace OpenRedding.Core.Salaries.Commands.DownloadSalaries
{
	using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Core.Data;
    using OpenRedding.Core.Extensions;
    using OpenRedding.Shared;

    public class DownloadSalariesCommandHandler : IRequestHandler<DownloadSalariesCommand, byte[]>
    {
        private readonly ILogger<DownloadSalariesCommandHandler> _logger;
        private readonly IOpenReddingDbContext _context;

        public DownloadSalariesCommandHandler(ILogger<DownloadSalariesCommandHandler> logger, IOpenReddingDbContext context) =>
            (_logger, _context) = (logger, context);

        public async Task<byte[]> Handle(DownloadSalariesCommand request, CancellationToken cancellationToken)
        {
            ArgumentValidation.CheckNotNull(request, nameof(request));
            ArgumentValidation.CheckNotNull(request.SearchRequest, nameof(request.SearchRequest));

            // Get a list of the user's current view of the data
            var results = await _context.Employees
                .AsNoTracking()
                .FromSearchRequest(request.SearchRequest)
                .Select(e => e.ToSalaryExportDto())
                .ToListAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}
