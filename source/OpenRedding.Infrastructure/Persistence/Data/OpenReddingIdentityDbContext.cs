namespace OpenRedding.Infrastructure.Persistence.Data
{
    using IdentityServer4.EntityFramework.Options;
    using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using OpenRedding.Infrastructure.Identity;

    public class OpenReddingIdentityDbContext : ApiAuthorizationDbContext<OpenReddingUser>
    {
        public OpenReddingIdentityDbContext(DbContextOptions<OpenReddingIdentityDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }
    }
}
