namespace OpenRedding.Infrastructure.Persistence.Factories
{
    using System;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using OpenRedding.Infrastructure.Persistence.Data;

    public class OpenReddingDbContextFactory : IDesignTimeDbContextFactory<OpenReddingDbContext>
    {
        private const string ConnectionStringName = "ConnectionString";

        public OpenReddingDbContext CreateDbContext(string[] args)
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

            var optionsBuilder = new DbContextOptionsBuilder<OpenReddingDbContext>();
            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                options.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                options.CommandTimeout(30);
            });

            return new OpenReddingDbContext(optionsBuilder.Options);
        }
    }
}