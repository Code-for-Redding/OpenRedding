namespace OpenRedding.Core.Data
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Salaries.Entities;
    using Microsoft.EntityFrameworkCore;
    using OpenRedding.Domain.Zoning.Entities;

    /// <summary>
    /// Offered ports for adding and saves changes to the database context for Entity Framework Core.
    /// </summary>
    public interface IOpenReddingDbContext
    {
        DbSet<Employee> Employees { get; set; }

        DbSet<ReddingZone> Zones { get; set; }

        Task BulkInsertEntitiesAsync<T>(IList<T> collectionToInsert, CancellationToken cancellationToken)
            where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
