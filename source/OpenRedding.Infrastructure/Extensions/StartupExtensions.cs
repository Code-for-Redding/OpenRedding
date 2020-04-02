namespace OpenRedding.Infrastructure.Extensions
{
    using System;
    using System.Reflection;
    using Core.Data;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using OpenRedding.Infrastructure.Identity;
    using OpenRedding.Infrastructure.Persistence.Data;
    using OpenRedding.Infrastructure.Persistence.Repositories;
    using OpenRedding.Infrastructure.Services;
    using OpenRedding.Shared.Validation;

    public static class StartupExtensions
    {
        public static void AddOpenReddingInfrastructure(this IServiceCollection services, IConfiguration configuration, bool addIdentityServer = false)
        {
            Validate.NotNull(configuration, nameof(configuration));

            var connectionString = configuration["ConnectionString"];
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

                // Add SendGrid
                var key = configuration["SendGridKey"];
                services.TryAddTransient<IEmailSender>(_ => new SendGridEmailSender(key));

                var builder = services.AddIdentityServer(options => options.Authentication.CookieLifetime = TimeSpan.FromHours(2))
                    .AddConfigurationStore(options => options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, DbContextOptions))
                    .AddOperationalStore(options => options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, DbContextOptions))
                    .AddAspNetIdentity<OpenReddingUser>();

                builder.AddDeveloperSigningCredential();

                services.AddAuthentication()
                    .AddIdentityServerJwt();
            }
        }
    }
}
