namespace OpenRedding.Infrastructure.Identity
{
    using Microsoft.AspNetCore.Identity;

    public class OpenReddingUser : IdentityUser
    {
        public string? ReasonForUse { get; set; }
    }
}
