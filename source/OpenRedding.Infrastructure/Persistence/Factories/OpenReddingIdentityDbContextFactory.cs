namespace OpenRedding.Infrastructure.Persistence.Factories
{
    using System;
    using System.Reflection;
    using IdentityServer4.EntityFramework.Options;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using OpenRedding.Infrastructure.Persistence.Data;

    public class OpenReddingIdentityDbContextFactory : IDesignTimeDbContextFactory<OpenReddingIdentityDbContext>
    {
        private const string ConnectionStringName = "ConnectionString";

        public OpenReddingIdentityDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<OpenReddingDbContext>()
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration[ConnectionStringName];

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException($"Connection string {ConnectionStringName} is null or empty", connectionString?.GetType().Name);
            }

            var optionsBuilder = new DbContextOptionsBuilder<OpenReddingIdentityDbContext>();
            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                options.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                options.CommandTimeout(30);
            });

            return new OpenReddingIdentityDbContext(optionsBuilder.Options, new OperationalStoreOptionsMigrations());
        }
    }

    public class OperationalStoreOptionsMigrations : IOptions<OperationalStoreOptions>
    {
        public OperationalStoreOptions Value => new OperationalStoreOptions()
        {
            DeviceFlowCodes = new TableConfiguration("DeviceCodes"),
            EnableTokenCleanup = false,
            PersistedGrants = new TableConfiguration("PersistedGrants"),
            TokenCleanupBatchSize = 100,
            TokenCleanupInterval = 3600,
        };
    }
}
