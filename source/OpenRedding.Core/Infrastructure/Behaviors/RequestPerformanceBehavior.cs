namespace OpenRedding.Core.Infrastructure.Behaviors
{
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Shared;

    public class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        // private readonly ICurrentUserService _currentUserService;
        public RequestPerformanceBehavior(ILogger<TRequest> logger)
        {
            // _currentUserService = currentUserService;
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // ArgumentValidation.ValidateNotNull(request, next);
            _timer.Start();

            var response = await next.Invoke();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > OpenReddingConstants.RequestElapsedMillisecondTimeWarningLimit)
            {
                var name = typeof(TRequest).Name;

                // _currentUserService.UserId,
                _logger.LogWarning(
                    "Open Redding long running request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                    name,
                    _timer.ElapsedMilliseconds,
                    request);
            }

            return response;
        }
    }
}