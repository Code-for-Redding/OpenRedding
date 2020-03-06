namespace OpenRedding.Core.Tests.Infrastructure
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using OpenRedding.Infrastructure.Persistence;
    using OpenRedding.Infrastructure.Persistence.Data;

    public static class OpenReddingDbContextTestFactory
    {
        public static async Task<OpenReddingDbContext> Create()
        {
            var options = new DbContextOptionsBuilder<OpenReddingDbContext>()
                .UseInMemoryDatabase("OpenRedding.Core.Tests.Db")
                .Options;

            var context = new OpenReddingDbContext(options);
            context.Database.EnsureCreated();
            await OpenReddingDatabaseInitializer.Initialize(context);

            return context;
        }

        public static void Destroy(OpenReddingDbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context), "Database context is null, cannot dispose");
            }

            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}