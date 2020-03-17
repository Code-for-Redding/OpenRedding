namespace OpenRedding.Validation.Extensions
{
	using System;
	using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    public static class StartupExtensions
    {
        public static void AddOpenReddingValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
