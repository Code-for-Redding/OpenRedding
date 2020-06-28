namespace OpenRedding.Core.Infrastructure.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IZoningTableSeeder
    {
        Task SeedZoningDataAsync(CancellationToken cancellationToken);
    }
}
