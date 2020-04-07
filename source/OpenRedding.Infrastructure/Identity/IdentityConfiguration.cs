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
                    }
                }
            };

        public static IEnumerable<Client> ApiClients =>
            new Client[]
            {
                new Client
                {
                    ClientId = OpenReddingIdentityConstants.BlazorClientId,
                    ClientName = "Open Redding SPA Client",
                    ClientUri = "http://localhost:5001",
                    ClientSecrets = { new Secret("test") },

                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { "openid", "profile", "email", OpenReddingIdentityConstants.OpenReddingReadScope },
                    RequirePkce = false,
                    RequireClientSecret = false,
                    PostLogoutRedirectUris = { "https://localhost:5001" },
                    AllowedCorsOrigins = { "https://localhost:5001" },
                    RedirectUris =
                    {
                        "https://localhost:5003"
                    }
                }
            };
    }
}
