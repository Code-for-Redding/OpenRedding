namespace OpenRedding.Identity.Middleware
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Domain.Common.Validation;

    /// <summary>
    /// Creates a pipeline validator to validate each request coming into the application layer.
    /// </summary>
    /// <typeparam name="TRequest">Request handler command or query.</typeparam>
    /// <typeparam name="TResponse">Excepted view model response from the associated handler.</typeparam>
    public class IdentityRequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : OpenReddingIdentityRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<TRequest> _logger;

        public IdentityRequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count > 0)
            {
                _logger.LogInformation($"Validation failure for request [{request}]");
                var validationErrors = new OpenReddingValidationErrors();

                foreach (var validationFailure in failures.Select(failure => new OpenReddingValidationError(failure.PropertyName, failure.ErrorMessage)))
                {
                    validationErrors.Errors.Add(validationFailure);
                }

                request.ValidationErrors = validationErrors;
            }

            return await next.Invoke();
        }
    }
}
