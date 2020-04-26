namespace OpenRedding.Shared.Identity
{
    public static class OpenReddingIdentityConstants
    {
        // TODO: If this client ID is changed, you must updated appsettings.json in the
        // OpenRedding.Client project for configuration purposes
        public const string BlazorClientId = "OpenRedding.Client";
        public const string OpenReddingApiClientId = "OpenRedding.Api";
        public const string OpenReddingReadScope = "read:open_redding_api";

        public static readonly string[] Scopes =
        {
            "read:open_redding_api",
            "openid",
            "email",
            "profile"
        };
    }
}
