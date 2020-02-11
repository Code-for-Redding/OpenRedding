namespace OpenRedding.Domain.Common.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDatabaseSeeder
    {
        Task SeedAsync(CancellationToken cancellationToken);
    }
}