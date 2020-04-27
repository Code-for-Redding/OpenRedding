namespace OpenRedding.Infrastructure.Identity
{
    using System.Collections.Generic;
    using IdentityServer4.Models;
    using OpenRedding.Shared.Identity;

    public static class IdentityConfiguration
    {
        public static IEnumerable<IdentityResource> Resources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource(OpenReddingIdentityConstants.OpenReddingApiClientId, "Open Redding API")
                {
                    Scopes =
                    {
                        new Scope(OpenReddingIdentityConstants.OpenReddingReadScope),
                        new Scope(new IdentityResources.OpenId().Name),
                        new Scope(new IdentityResources.Profile().Name),
                        new Scope(new IdentityResources.Email().Name)
                    },

                    ApiSecrets = { new Secret("test") }
                }
            };

        public static IEnumerable<Client> ApiClients =>
            new Client[]
            {
                new Client
                {
                    ClientId = OpenReddingIdentityConstants.BlazorClientId,
                    ClientName = "Open Redding SPA Client",
                    ClientUri = "https://localhost:5001",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile", "email", OpenReddingIdentityConstants.OpenReddingReadScope },
                    AllowedCorsOrigins = { "https://localhost:5001" },
                    PostLogoutRedirectUris = { "https://localhost:5001/" },
                    RedirectUris = { "https://localhost:5001/" },
                    RequireConsent = false
                }
            };
    }
}
