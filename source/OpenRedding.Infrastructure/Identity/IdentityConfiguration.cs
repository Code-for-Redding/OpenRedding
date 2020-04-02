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
                new ApiResource("open_redding_api", "Open Redding API")
                {
                    Scopes = { new Scope(OpenReddingIdentityConstants.OpenReddingReadScope) }
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

                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { "openid", "profile", "email", OpenReddingIdentityConstants.OpenReddingReadScope },
                    RequirePkce = true,
                    RequireClientSecret = false,
                    PostLogoutRedirectUris = { "http://localhost:5003" },
                    AllowedCorsOrigins = { "http://localhost:5003" },
                    RedirectUris =
                    {
                        "http://localhost:5003/index.html",
                        "http://localhost:5003/callback.html",
                        "http://localhost:5003/silent.html",
                        "http://localhost:5003/popup.html",
                    }
                }
            };
    }
}
