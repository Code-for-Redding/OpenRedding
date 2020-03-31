namespace OpenRedding.Identity.Models
{
	using OpenRedding.Infrastructure.Identity;

    public class ConfirmedRegisteredAccountDto
    {
        public ConfirmedRegisteredAccountDto(bool requireConfirmedAccount, OpenReddingUser user) =>
            (RequireConfirmedAccount, User) = (requireConfirmedAccount, user);

        public bool RequireConfirmedAccount { get; }

        public OpenReddingUser User { get; }
    }
}
