namespace OpenRedding.Domain.Accounts.Services
{
    using System.Threading.Tasks;

    public interface IOpenReddingAuthenticationService
    {
        Task<string?> GetRequestToken();
    }
}
