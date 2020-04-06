namespace OpenRedding.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using IdentityModel.Client;
    using Microsoft.AspNetCore.Components.Authorization;
    using OpenRedding.Shared.Identity;

    public class OpenReddingAuthenticationStateProvider : AuthenticationStateProvider
    {
        private const string AccessTokenKey = "access_token";

        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public OpenReddingAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
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

            if (clientToken?.IsError != false)
            {
                Console.WriteLine("No token returnped, unauthorized request attempt");
                return string.Empty;
            }

            Console.WriteLine("Token request successful, caching access token");
            await _localStorageService.SetItemAsync(AccessTokenKey, clientToken.AccessToken);

            return clientToken.AccessToken;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var defaultState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            Console.WriteLine("Retrieving authentication token");
            var cachedAccessToken = await GetRequestToken();

            if (string.IsNullOrWhiteSpace(cachedAccessToken))
            {
                Console.WriteLine("No cached token found");
                return defaultState;
            }

            // Attach the default header on all requests
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cachedAccessToken);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(cachedAccessToken), "jwt")));
        }

        public void MarkUserAsAuthenticated(string email)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs is null)
            {
                return claims;
            }

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("[", StringComparison.CurrentCultureIgnoreCase))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    if (!(parsedRoles is null))
                    {
                        foreach (var parsedRole in parsedRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                        }
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}
