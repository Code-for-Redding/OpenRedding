namespace OpenRedding.Infrastructure.Services
{
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;
	using CsvHelper;
	using Domain.Salaries.Dtos;
	using Domain.Salaries.Entities;
	using Microsoft.Extensions.Logging;
	using OpenRedding.Core.Data;
	using OpenRedding.Core.Extensions;
	using OpenRedding.Core.Infrastructure.Services;
	using OpenRedding.Infrastructure.Mappings;
	using Shared;

	public class SalaryTableSeeder : ISalaryTableSeeder
	{
		private readonly IOpenReddingDbContext _context;
		private readonly ILogger<SalaryTableSeeder> _logger;
		private readonly HttpClient _httpClient;

		public SalaryTableSeeder(IOpenReddingDbContext context, ILogger<SalaryTableSeeder> logger, HttpClient httpClient)
		{
			_context = context;
			_logger = logger;
			_httpClient = httpClient;
		}

		/// <summary>
		/// Seeds the employee salary table using CsvHelper to parse each CSV stream returned from calling the TP endpoints.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token provided by MediatR.</param>
		/// <returns>Completed Task.</returns>
		/// <exception cref="InvalidDataException">Throws if no data was added to the collection for bulk insertion.</exception>
		public async Task SeedAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Pulling data from Transparent California...");
			var salariedEmployees = new List<Employee>();

			foreach (var url in OpenReddingConstants.Urls)
			{
				// Call the endpoint to make sure we get the CSV data back
				_logger.LogInformation($"Calling CSV endpoint: {url}...");
				var response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
				response.EnsureSuccessStatusCode();

				_logger.LogInformation("Call was successful, reading from stream...");
				await using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

				while (stream.CanRead)
				{
					// While the stream is open and readable, consume the stream using CsvReader
					using var reader = new StreamReader(stream);
					using var csvReader = new CsvReader(reader, CultureInfo.CurrentCulture);

					// Configure the class map to properly read from the column headers
					csvReader.Configuration.RegisterClassMap<CsvEmployeeReadMap>();
					var employeeRecords = csvReader.GetRecordsAsync<TransparentCaliforniaCsvReadEmployeeDto>();

					await foreach (var employee in employeeRecords.WithCancellation(cancellationToken).ConfigureAwait(false))
					{
						// Map to an employee entity and add it to the collection for bulk inserting
						var salariedEmployee = employee.ToEmployee();
						salariedEmployees.Add(salariedEmployee);
					}
				}
			}

			if (salariedEmployees.Count is 0)
			{
				throw new InvalidDataException("Employee salary records was not populated, please check the data source");
			}

			// Add the collection and update the database
			_logger.LogInformation($"Seeding employee data, {salariedEmployees.Count} rows...");
			await _context.BulkInsertEntitiesAsync(salariedEmployees, cancellationToken).ConfigureAwait(false);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
			_logger.LogInformation("Data has been seeded successfully!");
		}
	}
}