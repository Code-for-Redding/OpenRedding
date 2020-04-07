namespace OpenRedding.Shared.Identity
{
    public static class OpenReddingIdentityConstants
    {
        public const string BlazorClientId = "open_redding_ui";
        public const string OpenReddingApiClientId = "open_redding_api";
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
