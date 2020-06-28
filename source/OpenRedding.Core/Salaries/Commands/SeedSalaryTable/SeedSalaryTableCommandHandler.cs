namespace OpenRedding.Core.Salaries.Commands.SeedSalaryTable
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Core.Infrastructure.Services;

    public class SeedSalaryTableCommandHandler : IRequestHandler<SeedSalaryTableCommand, Unit>
    {
        private readonly ILogger<SeedSalaryTableCommandHandler> _logger;
        private readonly ISalaryTableSeeder _seeder;

        public SeedSalaryTableCommandHandler(ILogger<SeedSalaryTableCommandHandler> logger, ISalaryTableSeeder seeder)
        {
            _logger = logger;
            _seeder = seeder;
        }

        public async Task<Unit> Handle(SeedSalaryTableCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to seed salary table...");
            await _seeder.SeedSalaryDataAsync(cancellationToken).ConfigureAwait(false);
            _logger.LogInformation("Seed was successful!");

            return Unit.Value;
        }
    }
}
