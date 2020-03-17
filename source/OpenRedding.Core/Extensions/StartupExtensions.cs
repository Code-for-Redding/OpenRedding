namespace OpenRedding.Core.Extensions
{
    using System.Reflection;
    using Data;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using OpenRedding.Core.Infrastructure.Behaviors;

    public static class StartupExtensions
    {
        public static void AddOpenReddingCore(this IServiceCollection services)
        {
            // Register MediatR handlers and request validators
            var executingAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(typeof(IOpenReddingDbContext).Assembly);
            services.AddValidatorsFromAssembly(executingAssembly);

            // Add the MediatR validation pipeline
            services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
        }
    }
}
