namespace OpenRedding.Identity
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using IdentityServer4.EntityFramework.DbContexts;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Serilog.Events;
    using Serilog.Sinks.SystemConsole.Themes;

#pragma warning disable RCS1102 // Make class static.

    public class Program
#pragma warning restore RCS1102 // Make class static.
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()

                // uncomment to write to Azure diagnostics stream
                // .WriteTo.File(
                //    @"D:\home\LogFiles\Application\identityserver.txt",
                //    fileSizeLimitBytes: 1_000_000,
                //    rollOnFileSizeLimit: true,
                //    shared: true,
                //    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            if (args.Contains("migrate"))
            {
                // Issue the command to seed and create the schema and populate the database
                using var scope = host.Services.CreateScope();

                try
                {
                    var configurationDbContext = scope.ServiceProvider.GetService<ConfigurationDbContext>();
                    var persistedGrandDbContext = scope.ServiceProvider.GetService<PersistedGrantDbContext>();

                    // Drop the tables to recreate them with fresh data every server re-roll
                    if (configurationDbContext.Database.CanConnect())
                    {
                        Log.Information("Applying migrations...");
                        await configurationDbContext.Database.MigrateAsync();
                        await persistedGrandDbContext.Database.MigrateAsync();
                        Log.Information("Migration complete, populating database with data from Transparent California...");
                    }
                    else
                    {
                        throw new SystemException("Could not connect to database, please check that the service is up and running");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Could not seed database");
                }
            }

            try
            {
                Log.Information("Starting host...");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog();
                });
    }
}