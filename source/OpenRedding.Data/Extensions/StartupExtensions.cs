namespace OpenRedding.Data.Extensions
{
    using Core.Data;
    using Domain.Common.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Repositories;

    public static class StartupExtensions
    {
        public static void AddOpenReddingPersistence(this IServiceCollection services, string connectionString)
        {
            // Add EF Core dependencies
            services.AddDbContext<OpenReddingDbContext>(options =>
                options.UseSqlServer(connectionString, builder =>
                {
                    builder.EnableRetryOnFailure(2);
                    builder.MigrationsAssembly(typeof(OpenReddingDbContext).Assembly.GetName().Name);
                }));
            services.TryAddScoped<IOpenReddingDbContext>(provider => provider.GetService<OpenReddingDbContext>());

            // Add Dapper dependencies
            services.TryAddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));
        }
    }
}