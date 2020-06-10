namespace OpenRedding.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Azure.Storage.Blobs;
    using CsvHelper;
    using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Core.Infrastructure.Services;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Shared;

    public class SalaryCsvBlobService : IAzureBlobService
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly ILogger<SalaryCsvBlobService> _logger;

        public SalaryCsvBlobService(IConfiguration configuration, ILogger<SalaryCsvBlobService> logger)
        {
            _logger = logger;

            if (string.IsNullOrWhiteSpace(configuration["AzureStorageConnectionString"]))
            {
                throw new ArgumentException("Azure Storage connection string is null");
            }

            if (string.IsNullOrWhiteSpace(configuration["SalaryCsvContainer"]))
            {
                throw new ArgumentException("Salary CSV Container Name is null");
            }

            _logger.LogInformation("Instantiating service client...");
            var blobServiceClient = new BlobServiceClient(configuration["AzureStorageConnectionString"]);
            _blobContainerClient = blobServiceClient.GetBlobContainerClient(configuration["SalaryCsvContainer"]);
            _logger.LogInformation("Service client successfully instantiated!");
        }

        public async Task<OpenReddingLink> CreateBlobWithContents(IEnumerable<object>? results, CancellationToken cancellationToken)
        {
            ArgumentValidation.CheckNotNull(results, nameof(results));

            // Create a temp folder
            _logger.LogInformation("Creating temporary file resources...");
            string tempFolder = $"{Directory.GetCurrentDirectory()}\\Temp";
            string fileName = "salaries-" + Guid.NewGuid().ToString() + ".csv";
            string tempFilePath = Path.Combine(tempFolder, fileName);
            _logger.LogInformation($"File to create: {tempFilePath}");

            if (!Directory.Exists(tempFolder))
            {
                _logger.LogInformation($"{tempFolder} directory does not exist, creating...");
                Directory.CreateDirectory(tempFolder);
                _logger.LogInformation($"{tempFolder} directory created successfully!");
            }

            // Write text to the file
            _logger.LogInformation($"Writing file contents to {tempFilePath}");
            using var writer = new StreamWriter(tempFilePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(results);

            // Dispose of stream resources
            await writer.DisposeAsync();
            await csv.DisposeAsync();
            _logger.LogInformation($"CSV file created successfully, uploading blob to container: {_blobContainerClient.Name}");

            // Get a reference to a blob
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);

            // Open the file and upload its data
            _logger.LogInformation($"Updated file contents to blob location: {blobClient.Uri}");
            using FileStream uploadFileStream = File.OpenRead(tempFilePath);
            await blobClient.UploadAsync(uploadFileStream, true, cancellationToken: cancellationToken);
            uploadFileStream.Close();
            _logger.LogInformation("Blob successfully uploaded, cleaning up temporary resources...");

            // Clean up temporary resources
            File.Delete(tempFilePath);

            foreach (var file in Directory.GetFiles(tempFolder))
            {
                _logger.LogInformation($"Additional files detected in {tempFolder}, removing file {file}...");
                File.Delete(file);
                _logger.LogInformation($"{file} removed successfully!");
            }

            _logger.LogInformation($"Deleting temporary directory {tempFolder}...");
            Directory.Delete(tempFolder);
            _logger.LogInformation($"{tempFolder} successfully removed, returning blob URI: {blobClient.Uri.AbsoluteUri}");

            // Return the link to the blob
            return new OpenReddingLink
            {
                Href = blobClient.Uri.AbsoluteUri,
                Method = nameof(HttpMethod.Get),
            };
        }

        public async Task DehydrateBlob(CancellationToken cancellationToken)
        {
            // Set the scrub time to one hour
            var scrubTime = DateTimeOffset.Now.AddHours(-1);

            _logger.LogInformation($"Attempting to dehydrate container {_blobContainerClient.Name}...");
            await foreach (var blob in _blobContainerClient.GetBlobsAsync(cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                if (!blob.Properties.CreatedOn.HasValue || DateTimeOffset.Compare(scrubTime, blob.Properties.CreatedOn.Value) > 0)
                {
                    _logger.LogInformation($"Long lived CSV found {blob.Name}, removing from container {_blobContainerClient.Name}...");
                    await _blobContainerClient.DeleteBlobIfExistsAsync(blob.Name, cancellationToken: cancellationToken).ConfigureAwait(false);
                    _logger.LogInformation($"CSV blob {blob.Name} successfully removed from container {_blobContainerClient.Name}!");
                }
            }

            _logger.LogInformation($"Container {_blobContainerClient.Name} successfully dehydrated!");
        }
    }
}
