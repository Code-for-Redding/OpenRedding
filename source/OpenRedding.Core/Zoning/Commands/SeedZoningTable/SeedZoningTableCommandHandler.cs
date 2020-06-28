namespace OpenRedding.Core.Zoning.Commands.SeedZoningTable
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Core.Infrastructure.Services;

    public class SeedZoningTableCommandHandler : IRequestHandler<SeedZoningTableCommand, Unit>
    {
        private readonly ILogger<SeedZoningTableCommandHandler> _logger;
        private readonly IZoningTableSeeder _seeder;

        public SeedZoningTableCommandHandler(ILogger<SeedZoningTableCommandHandler> logger, IZoningTableSeeder seeder)
        {
            _logger = logger;
            _seeder = seeder;
        }

        public async Task<Unit> Handle(SeedZoningTableCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to seed zoning table...");
            await _seeder.SeedZoningDataAsync(cancellationToken).ConfigureAwait(false);
            _logger.LogInformation("Seed was successful!");

            return Unit.Value;
        }
    }
}
