namespace OpenRedding.Api
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Core.Salaries.Commands.SeedSalaryTable;
    using OpenRedding.Core.Zoning.Commands.SeedZoningTable;
    using OpenRedding.Infrastructure.Persistence.Data;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            if (args.Contains("seed"))
            {
                // Issue the command to seed and create the schema and populate the database
                using var scope = host.Services.CreateScope();

                try
                {
                    var context = scope.ServiceProvider.GetService<OpenReddingDbContext>();
                    var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
                    var mediator = scope.ServiceProvider.GetService<IMediator>();

                    // Drop the tables to recreate them with fresh data every server re-roll
                    if (context.Database.CanConnect())
                    {
                        var timer = new Stopwatch();
                        timer.Start();

                        var dbConnection = context.Database.GetDbConnection();
                        if (dbConnection.State.Equals(ConnectionState.Closed))
                        {
                            await dbConnection.OpenAsync();
                        }

                        await DropTableIfExistsAsync("__EFMigrationsHistory", dbConnection, logger);
                        await DropTableIfExistsAsync(nameof(context.Employees), dbConnection, logger);
                        await DropTableIfExistsAsync(nameof(context.Zones), dbConnection, logger);

                        logger.LogInformation("Applying migrations...");
                        await context.Database.MigrateAsync();
                        logger.LogInformation("Migration complete, populating database with database tables...");

                        await mediator.Send(new SeedSalaryTableCommand());
                        await mediator.Send(new SeedZoningTableCommand());
                        timer.Stop();
                        logger.LogInformation($"Database seeding was successful, time taken: {timer.Elapsed.TotalSeconds} seconds");
                    }
                    else
                    {
                        throw new SystemException("Could not connect to database, please check that the service is up and running");
                    }
                }
                catch (Exception e)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "Could not seed database");
                }
            }

            try
            {
                host.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Host terminated unexpectedly. Reason: {e.Message}");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

        private static async Task DropTableIfExistsAsync(string table, DbConnection dbConnection, ILogger<Program> logger)
        {
#pragma warning disable CA2100
            // Grab a disposable instance of the connection and query for the existing table
            using var command = dbConnection.CreateCommand();

            // Join the tables information and schema tables
            command.CommandText = @$"
SELECT 1 FROM sys.tables AS T
    INNER JOIN sys.schemas AS S ON T.schema_id = S.schema_id
WHERE S.Name = 'dbo' AND T.Name = '{table}'";

            // Drop the table if found, effectively re-hydrating the data
            if (await command.ExecuteScalarAsync() != null)
            {
                logger.LogInformation($"Dropping table {table} for re-hydration...");
                command.CommandText = $@"
DROP TABLE [dbo].[{table}]";
                await command.ExecuteScalarAsync();
            }
#pragma warning restore CA2100
        }
    }
}
