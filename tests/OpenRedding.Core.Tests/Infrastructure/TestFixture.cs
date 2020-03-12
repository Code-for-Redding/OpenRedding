namespace OpenRedding.Core.Tests.Infrastructure
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using OpenRedding.Infrastructure.Persistence;
    using OpenRedding.Infrastructure.Persistence.Data;

    public class TestFixture : IDisposable
    {
        public TestFixture()
        {
            // var connectionString = "Server=(localdb)\\mssqllocaldb;Database=OpenRedding;Trusted_Connection=True;MultipleActiveResultSets=true;Application Name=OpenRedding;";
            // Configure services
            var services = new ServiceCollection();

            services.AddEntityFrameworkInMemoryDatabase()
               .AddDbContext<OpenReddingDbContext>(options => options.UseInMemoryDatabase($"{Guid.NewGuid().ToString()}.db"));

            // services.AddDbContext<OpenReddingDbContext>(options => options.UseSqlServer(connectionString));

            // Configure current user accessor as a provider
            var serviceProvider = services.BuildServiceProvider();

            // Initialize the database with seed data and context accessors services
            var databaseContext = serviceProvider.GetRequiredService<OpenReddingDbContext>();
            databaseContext.Database.EnsureCreated();
            OpenReddingDatabaseInitializer.Initialize(databaseContext).Wait();

            Context = databaseContext;
        }

        protected OpenReddingDbContext Context { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Database.EnsureDeleted();
                Context.Dispose();
            }
        }
    }
}
