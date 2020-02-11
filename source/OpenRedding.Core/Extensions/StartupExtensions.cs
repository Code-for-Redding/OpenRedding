namespace OpenRedding.Core.Extensions
{
    using Data;
    using FluentValidation;
    using Infrastructure;
    using MediatR;
    using MediatR.Pipeline;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class StartupExtensions
    {
        public static void AddOpenReddingCore(this IServiceCollection services)
        {
            // Register MediatR handlers and request validators
            services.AddMediatR(typeof(IOpenReddingDbContext).Assembly);
            services.AddValidatorsFromAssembly(typeof(IOpenReddingDbContext).Assembly);

            // Add the MediatR validation pipeline
            services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
        }
    }
}