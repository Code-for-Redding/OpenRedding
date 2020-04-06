namespace OpenRedding.Client.Services
{
	using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using IdentityModel.Client;
    using OpenRedding.Domain.Accounts.Services;
    using OpenRedding.Shared.Identity;

    public class OpenReddingAuthenticationService : IOpenReddingAuthenticationService
    {
        private const string AccessTokenKey = "access_token";
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public OpenReddingAuthenticationService(HttpClient httpClient2, ILocalStorageService localStorageService2)
        {
            _httpClient = httpClient2;
            _localStorageService = localStorageService2;
        }

        public async Task<string?> GetRequestToken()
        {
            Console.WriteLine("Retrieving cached key...");
            if (await _localStorageService.ContainKeyAsync(AccessTokenKey))
            {
                Console.WriteLine("Cached token found");
                return await _localStorageService.GetItemAsync<string?>(AccessTokenKey);
            }

            Console.WriteLine("No token found, attempting call for access token...");
            using var clientProperties = new TokenRequest
            {
                Address = "https://localhost:5003/connect/token",
                GrantType = "code",

                ClientId = OpenReddingIdentityConstants.BlazorClientId,

                Parameters =
                {
                    { "scope", OpenReddingIdentityConstants.OpenReddingReadScope }
                }
            };

            Console.WriteLine("Calling token endpoint...");
            var clientToken = await _httpClient.RequestTokenAsync(clientProperties);

            if (clientToken is null)
            {
                Console.WriteLine("No token returned, unauthorized request attempt");
                throw new UnauthorizedAccessException("Unauthorized request attempt");
            }

            Console.WriteLine("Token request successful, caching access token");
            await _localStorageService.SetItemAsync(AccessTokenKey, clientToken.AccessToken);

            return clientToken.AccessToken;
        }
    }
}
