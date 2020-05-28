namespace OpenRedding.Core.Salaries.Commands.DownloadSalaries
{
	using System;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Core.Data;
    using OpenRedding.Core.Exception;
    using OpenRedding.Core.Extensions;
    using OpenRedding.Core.Infrastructure.Services;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Shared;

    public class DownloadSalariesCommandHandler : IRequestHandler<DownloadSalariesCommand, OpenReddingLink>
    {
        private readonly ILogger<DownloadSalariesCommandHandler> _logger;
        private readonly IOpenReddingDbContext _context;
        private readonly IAzureBlobService _blobService;

        public DownloadSalariesCommandHandler(ILogger<DownloadSalariesCommandHandler> logger, IOpenReddingDbContext context, IAzureBlobService blobService) =>
            (_logger, _context, _blobService) = (logger, context, blobService);

        public async Task<OpenReddingLink> Handle(DownloadSalariesCommand request, CancellationToken cancellationToken)
        {
            ArgumentValidation.CheckNotNull(request, nameof(request));
            ArgumentValidation.CheckNotNull(request.SearchRequest, nameof(request.SearchRequest));

            // Get a list of the user's current view of the data
            var results = await _context.Employees
                .AsNoTracking()
                .FromSearchRequest(request.SearchRequest)
                .Select(e => e.ToSalaryExportDto())
                .ToListAsync(cancellationToken);

            // Create the CSV blob in blob storage
            var linkToBlob = await _blobService.CreateBlobWithContents(results, cancellationToken);

            if (linkToBlob is null || string.IsNullOrWhiteSpace(linkToBlob.Href))
            {
                throw new OpenReddingApiException("Could not create the salary download file, please try the request again.", HttpStatusCode.InternalServerError);
            }

            await _blobService.DehydrateBlob(cancellationToken);

            return linkToBlob;
        }
    }
}
