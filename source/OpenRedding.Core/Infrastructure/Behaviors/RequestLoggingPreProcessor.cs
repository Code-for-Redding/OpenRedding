namespace OpenRedding.Core.Infrastructure.Behaviors
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR.Pipeline;
    using Microsoft.Extensions.Logging;

    public class RequestLoggingPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<TRequest> _logger;

        // private readonly ICurrentUserService _currentUserService;
        public RequestLoggingPreProcessor(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation("Open Redding Request: {Name} {@Request}", requestName, request);

            // _logger.LogInformation("Northwind Request: {Name} {@UserId} {@Request}",
            //     name, _currentUserService.UserId, request);
            return Task.CompletedTask;
        }
    }
}
