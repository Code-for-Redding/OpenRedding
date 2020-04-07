namespace OpenRedding.Domain.Accounts.Services
{
    using System.Threading.Tasks;

    public interface IOpenReddingOAuth2Service
    {
        Task<string?> GetAccessToken();

        Task<string?> GetCachedAccessToken(bool intializeTokeRequest = false);

        Task<string?> GetAuthorizationCode();
    }
}
