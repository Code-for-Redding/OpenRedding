namespace OpenRedding.Core.Infrastructure.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISalaryTableSeeder
    {
        Task SeedAsync(CancellationToken cancellationToken);
    }
}