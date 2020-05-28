namespace OpenRedding.Core.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenRedding.Domain.Common.Miscellaneous;

    public interface IAzureBlobService
    {
        Task<OpenReddingLink> CreateBlobWithContents(IEnumerable<object>? results, CancellationToken cancellationToken);

        Task DehydrateBlob(CancellationToken cancellationToken);
    }
}
