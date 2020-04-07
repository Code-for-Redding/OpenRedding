namespace OpenRedding.Domain.Accounts.Services
{
    using System.Threading.Tasks;

    public interface IOpenReddingOAuth2Service
    {
        Task<string?> GetActivationCode();

        Task<string?> GetAccessToken();
    }
}
