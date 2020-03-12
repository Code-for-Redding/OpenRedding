namespace OpenRedding.Infrastructure.Identity
{
    using System.Collections.Generic;
    using IdentityServer4.Models;

    public static class IdentityConfiguration
    {
        private const string OpenReddingReadScope = "read:open_redding_api";

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
                new ApiResource("open_redding_api", "Open Redding API")
                {
                    Scopes = { new Scope(OpenReddingReadScope) }
                }
            };

        public static IEnumerable<Client> ApiClients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "open_redding_ui",
                    ClientName = "Open Redding SPA Client",
                    ClientUri = "http://localhost:5002",

                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { "openid", "profile", "email", OpenReddingReadScope },
                    RequirePkce = true,
                    RequireClientSecret = false,
                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5002" },
                    RedirectUris =
                    {
                        "http://localhost:5002/index.html",
                        "http://localhost:5002/callback.html",
                        "http://localhost:5002/silent.html",
                        "http://localhost:5002/popup.html",
                    }
                }
            };
    }
}
