namespace OpenRedding.Client
{
    using System.Reflection;
    using System.Threading.Tasks;
    using Fluxor;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddFluxor(options =>
            {
                options.ScanAssemblies(Assembly.GetExecutingAssembly());
                options.UseReduxDevTools();
            });

            await builder
                .Build()
                .RunAsync();
        }
    }
}
