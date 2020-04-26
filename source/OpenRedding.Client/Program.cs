namespace OpenRedding.Client
{
    using System;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using Fluxor;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using OpenRedding.Client.Services;
    using OpenRedding.Domain.Accounts.Services;
    using OpenRedding.Shared.Identity;

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            const string OpenReddingDomain = "https://localhost:5001/";
            const string IdentityServerDomain = "https://localhost:5003";

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // Add authorization services
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddLogging();
            builder.Services.AddTransient(_ => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Local", options.ProviderOptions);

                options.AuthenticationPaths.LogInPath = $"{IdentityServerDomain}/identity/account/login?returnUrl={OpenReddingDomain}";
                options.AuthenticationPaths.LogInCallbackPath = OpenReddingDomain;
                options.AuthenticationPaths.LogInFailedPath = $"{IdentityServerDomain}/identity/account/login?returnUrl={OpenReddingDomain}";

                options.AuthenticationPaths.LogOutPath = $"{IdentityServerDomain}/identity/account/logout";
                options.AuthenticationPaths.LogOutCallbackPath = OpenReddingDomain;
                options.AuthenticationPaths.LogOutFailedPath = OpenReddingDomain;
                options.AuthenticationPaths.LogOutSucceededPath = OpenReddingDomain;

                options.AuthenticationPaths.RegisterPath = $"{IdentityServerDomain}/identity/account/register";

                options.UserOptions.AuthenticationType = $"{IdentityServerDomain}/_configuration/{OpenReddingIdentityConstants.BlazorClientId}";

                options.ProviderOptions.Authority = IdentityServerDomain;
                options.ProviderOptions.ClientId = OpenReddingIdentityConstants.BlazorClientId;

                foreach (var scope in OpenReddingIdentityConstants.Scopes)
                {
                    options.ProviderOptions.DefaultScopes.Add(scope);
                }
            });

            builder.Services.AddFluxor(options =>
            {
                options.ScanAssemblies(Assembly.GetExecutingAssembly());
                options.UseReduxDevTools();
            });

            // Add custom services
            builder.Services.AddScoped<IOpenReddingOAuth2Service, OpenReddingOAuth2Service>();

            await builder
                .Build()
                .RunAsync();
        }
    }
}
