namespace OpenRedding.Infrastructure.Extensions
{
    using System;
    using System.Reflection;
    using Core.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using OpenRedding.Core.Infrastructure.Services;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Infrastructure.Persistence.Data;
    using OpenRedding.Infrastructure.Persistence.Repositories;
    using OpenRedding.Infrastructure.Services;
    using OpenRedding.Shared.Validation;

    public static class StartupExtensions
    {
        public static void AddOpenReddingInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            Validate.NotNull(configuration, nameof(configuration));

            var connectionString = configuration["ConnectionString"];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Database connection string is null");
            }

            var migrationsAssembly = Assembly.GetExecutingAssembly().FullName;
            void DbContextOptions(SqlServerDbContextOptionsBuilder builder)
            {
                // Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                builder.MigrationsAssembly(migrationsAssembly);
                builder.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            }

            // Add project dependencies
            services.AddDbContext<OpenReddingDbContext>(options => options.UseSqlServer(connectionString, DbContextOptions));
            services.TryAddScoped<IOpenReddingDbContext>(provider => provider.GetService<OpenReddingDbContext>());
            services.TryAddScoped<IUnitOfWork>(_ => new UnitOfWork(connectionString));
            services.TryAddScoped<IAzureBlobService, SalaryCsvBlobService>();
            services.TryAddScoped<ILinkBuilder<EmployeeSalarySearchResultDto>, SalariesLinkBuilder>();
        }
    }
}
