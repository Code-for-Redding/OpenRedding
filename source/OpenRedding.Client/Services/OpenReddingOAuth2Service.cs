namespace OpenRedding.Client.Services
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using IdentityModel.Client;
    using Microsoft.AspNetCore.Components;
    using OpenRedding.Domain.Accounts.Services;
    using OpenRedding.Shared.Identity;

    public class OpenReddingOAuth2Service : IOpenReddingOAuth2Service
    {
        private const string AccessTokenKey = "access_token";

        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly NavigationManager _navigationManager;

        public OpenReddingOAuth2Service(HttpClient httpClient, ILocalStorageService localStorageService, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
        }

        public async Task<string?> GetAccessToken()
        {
            Console.WriteLine("No token found, attempting call for authorization token...");
            await GetAuthorizationCode();

            Console.WriteLine("Initializing client token request...");
            using var clientProperties = new AuthorizationCodeTokenRequest
            {
                Address = "https://localhost:5003/connect/token",
                ClientId = OpenReddingIdentityConstants.BlazorClientId,
                RedirectUri = _navigationManager.BaseUri,
                GrantType = "code",
                ClientSecret = "test",
                Code = "code",

                Parameters =
                {
                    { "scope", OpenReddingIdentityConstants.OpenReddingReadScope }
                }
            };

            Console.WriteLine("Calling token endpoint...");
            var clientToken = await _httpClient.RequestAuthorizationCodeTokenAsync(clientProperties);

            if (clientToken?.IsError != false)
            {
                Console.WriteLine("No token returnped, unauthorized request attempt");
                return string.Empty;
            }

            Console.WriteLine("Token request successful, caching access token");
            await _localStorageService.SetItemAsync(AccessTokenKey, clientToken.AccessToken);

            return clientToken.AccessToken;
        }

        public async Task<string?> GetCachedAccessToken(bool initializeTokenRequest = false)
        {
            Console.WriteLine("Attempting to retrieve cached token...");
            if (await _localStorageService.ContainKeyAsync(AccessTokenKey))
            {
                Console.WriteLine("Cached token found");
                return await _localStorageService.GetItemAsync<string?>(AccessTokenKey);
            }

            return initializeTokenRequest ? await GetAccessToken() : string.Empty;
        }

        public async Task<string?> GetAuthorizationCode()
        {
            Console.WriteLine("Calling discovery endpoint...");
            var discoveryEndpointResponse = await _httpClient.GetDiscoveryDocumentAsync("https://localhost:5003/.well-known/openid-configuration");

            if (discoveryEndpointResponse?.IsError != false)
            {
                Console.WriteLine("No response form discover endpoint, unauthorized attempt");
                throw new UnauthorizedAccessException("Discovery endpoint was not available");
            }

            var scopes = OpenReddingIdentityConstants.Scopes
                .Aggregate((current, next) => $"{current} {next}");

            var requestUriWithQueryParameters = new StringBuilder(discoveryEndpointResponse.AuthorizeEndpoint)
                .Append("?client_id=").Append(OpenReddingIdentityConstants.BlazorClientId)
                .Append("&response_type=code")
                .Append("&state=").Append(Guid.NewGuid())
                .Append("&redirect_uri=").Append(_navigationManager.BaseUri)
                .Append("&scope=").Append(scopes);

            var authorizationCodeRequestUri = new Uri(requestUriWithQueryParameters.ToString());

            Console.WriteLine("Requesting authorization code...");
            var authorizationCodeResponse = await _httpClient.GetAsync(authorizationCodeRequestUri);
            authorizationCodeResponse.EnsureSuccessStatusCode();

            return string.Empty;
        }
    }
}
