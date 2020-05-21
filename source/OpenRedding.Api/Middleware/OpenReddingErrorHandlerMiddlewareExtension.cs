namespace OpenRedding.Api.Middleware
{
    using Microsoft.AspNetCore.Builder;

    public static class OpenReddingErrorHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UseConduitErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OpenReddingErrorHandler>();
        }
    }
}
