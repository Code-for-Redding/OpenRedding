namespace OpenRedding.Infrastructure.Extensions
{
    using System;
    using System.Reflection;
    using Core.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using OpenRedding.Infrastructure.Identity;
    using OpenRedding.Infrastructure.Persistence.Data;
    using OpenRedding.Infrastructure.Persistence.Repositories;

    public static class StartupExtensions
    {
        public static void AddOpenReddingInfrastructure(this IServiceCollection services, string connectionString, bool addIdentityServer = false)
        {
            var migrationsAssembly = Assembly.GetExecutingAssembly().GetName().Name;
            void DbContextOptions(SqlServerDbContextOptionsBuilder builder)
            {
                // Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                builder.MigrationsAssembly(migrationsAssembly);
                builder.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            }

            // Add EF Core dependencies
            services.AddDbContext<OpenReddingDbContext>(options => options.UseSqlServer(connectionString, DbContextOptions));
            services.TryAddScoped<IOpenReddingDbContext>(provider => provider.GetService<OpenReddingDbContext>());

            // Add Dapper dependencies
            services.TryAddScoped<IUnitOfWork>(_ => new UnitOfWork(connectionString));

            // Add custom services
            // services.AddHttpClient<ISalaryTableSeeder, SalaryTableSeeder>(options => options.Timeout = TimeSpan.FromSeconds(30));
            if (addIdentityServer)
            {
                // Add Identity and IS4
                services.AddIdentity<OpenReddingUser, IdentityRole>()
                    .AddEntityFrameworkStores<OpenReddingDbContext>()
                    .AddDefaultTokenProviders();

                var builder = services.AddIdentityServer(options => options.Authentication.CookieLifetime = TimeSpan.FromHours(2))
                    .AddConfigurationStore(options => options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, DbContextOptions))
                    .AddOperationalStore(options => options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, DbContextOptions))
                    .AddAspNetIdentity<OpenReddingUser>()
                    .AddInMemoryIdentityResources(IdentityConfiguration.Resources)
                    .AddInMemoryApiResources(IdentityConfiguration.Apis)
                    .AddInMemoryClients(IdentityConfiguration.ApiClients);

                builder.AddDeveloperSigningCredential();
            }
        }
    }
}
