namespace OpenRedding.Data
{
	using Microsoft.EntityFrameworkCore;

	public class OpenReddingDbContextFactory : DesignTimeDbContextFactoryBase<OpenReddingDbContext>
	{
		protected override OpenReddingDbContext CreateNewInstance(DbContextOptions<OpenReddingDbContext> options)
		{
			return new OpenReddingDbContext(options);
		}
	}
}