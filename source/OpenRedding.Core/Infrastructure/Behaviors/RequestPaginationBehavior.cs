namespace OpenRedding.Core.Infrastructure.Behaviors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Core.Infrastructure.Requests;
    using OpenRedding.Domain.Common.Validation;
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Shared;

    /// <summary>
    /// Creates the relevant links for the view model list.
    /// </summary>
    /// <typeparam name="TRequest">Request handler command or query.</typeparam>
    /// <typeparam name="TResponse">Excepted view model response from the associated handler.</typeparam>
    public class RequestPaginationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : OpenReddingRequest<TResponse>
        where TResponse : OpenReddingViewModelList
    {
        private readonly ILogger<TRequest> _logger;

        public RequestPaginationBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            ArgumentValidation.ValidateNotNull(request, next);

            return await next.Invoke();
        }
    }
}
