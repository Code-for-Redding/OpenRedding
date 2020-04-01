namespace OpenRedding.Identity.Models
{
	using OpenRedding.Infrastructure.Identity;

    public class RegisteredAccountDto
    {
        public RegisteredAccountDto(OpenReddingUser? user, bool requireConfirmedAccount = true) =>
            (User, RequireConfirmedAccount) = (user, requireConfirmedAccount);

        public OpenReddingUser? User { get; }

        public bool RequireConfirmedAccount { get; }
    }
}
