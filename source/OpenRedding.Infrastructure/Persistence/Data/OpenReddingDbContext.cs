namespace OpenRedding.Infrastructure.Persistence.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Data;
    using Domain.Salaries.Entities;
    using EFCore.BulkExtensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class OpenReddingDbContext : DbContext, IOpenReddingDbContext
    {
        private static readonly ILoggerFactory ConsoleLogger = LoggerFactory.Create(builder => builder.AddConsole());

        public OpenReddingDbContext(DbContextOptions<OpenReddingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = default!;

        public async Task BulkInsertEntitiesAsync<T>(IList<T> entities, CancellationToken cancellationToken)
            where T : class
        {
            await this.BulkInsertAsync(entities, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder), "Model building was null, check EF configuration");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OpenReddingDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder), "Configuration builder for DbContext was null");
            }

            optionsBuilder.UseLoggerFactory(ConsoleLogger);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
