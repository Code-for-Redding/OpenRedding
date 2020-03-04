namespace OpenRedding.Infrastructure.Extensions
{
    using System.Reflection;
    using Core.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using OpenRedding.Core.Infrastructure.Services;
    using OpenRedding.Infrastructure.Persistence.Contexts;
    using OpenRedding.Infrastructure.Persistence.Repositories;
    using OpenRedding.Infrastructure.Services;

    public static class StartupExtensions
    {
        public static void AddOpenReddingInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Add EF Core dependencies
            services.AddDbContext<OpenReddingDbContext>(options =>
                options.UseSqlServer(connectionString, builder =>
                {
                    builder.EnableRetryOnFailure(2);
                    builder.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                }));
            services.TryAddScoped<IOpenReddingDbContext>(provider => provider.GetService<OpenReddingDbContext>());

            // Add Dapper dependencies
            services.TryAddScoped<IUnitOfWork>(_ => new UnitOfWork(connectionString));

            // Add custom services
            // services.AddHttpClient<ISalaryTableSeeder, SalaryTableSeeder>(options => options.Timeout = TimeSpan.FromSeconds(30));
        }
    }
}