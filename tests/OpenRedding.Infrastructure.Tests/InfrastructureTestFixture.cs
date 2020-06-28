namespace OpenRedding.Core.Tests.Infrastructure
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging.Abstractions;
    using Moq;
    using OpenRedding.Core.Infrastructure.Services;
    using OpenRedding.Infrastructure.Services;

    public class InfrastructureTestFixture : IDisposable
    {
        public InfrastructureTestFixture()
        {
            // Instantiate our service container
            var services = new ServiceCollection();

            // Configure service dependency mocks
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Object["AzureStorageConnectionString"] = "TestConnectionString";
            mockConfiguration.Object["SalaryCsvContainer"] = "TestSalaryCsvContainer";

            // Add our services and build
            services.AddScoped<IAzureBlobService>(_ => new SalaryCsvBlobService(mockConfiguration.Object, NullLogger<SalaryCsvBlobService>.Instance));
            var serviceProvider = services.BuildServiceProvider();

            // Instantiate our test services
            TestBlobService = serviceProvider.GetRequiredService<IAzureBlobService>();
        }

        protected IAzureBlobService TestBlobService { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }
    }
}
