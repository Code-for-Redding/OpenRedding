namespace OpenRedding.Core.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IFileProvider<T>
        where T : class
    {
        Task CreateCsv(IEnumerable<T> recordsToWrite, CancellationToken cancellationToken);
    }
}
