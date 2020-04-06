namespace OpenRedding.Client
{
    using System.Reflection;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using Fluxor;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using OpenRedding.Client.Services;
    using OpenRedding.Domain.Accounts.Services;

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // Add authorization services
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddApiAuthorization(options => options.ProviderOptions.ConfigurationEndpoint = "https://localhost:5003/.well-known/openid-configuration");
            builder.Services.AddFluxor(options =>
            {
                options.ScanAssemblies(Assembly.GetExecutingAssembly());
                options.UseReduxDevTools();
            });

            // Add custom services
            builder.Services.AddScoped<IOpenReddingAuthenticationService, OpenReddingAuthenticationService>();

            await builder
                .Build()
                .RunAsync();
        }
    }
}
