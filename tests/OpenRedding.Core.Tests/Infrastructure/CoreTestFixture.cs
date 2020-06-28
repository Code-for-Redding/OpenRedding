namespace OpenRedding.Core.Tests.Infrastructure
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using OpenRedding.Core.Extensions;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.Entities;
    using OpenRedding.Domain.Salaries.Enums;
    using OpenRedding.Domain.Salaries.ViewModels;
    using OpenRedding.Infrastructure.Persistence;
    using OpenRedding.Infrastructure.Persistence.Data;

    public class CoreTestFixture : IDisposable
    {
        public CoreTestFixture()
        {
            // Configure services
            var services = new ServiceCollection();

            services.AddEntityFrameworkInMemoryDatabase()
               .AddDbContext<OpenReddingDbContext>(options => options.UseInMemoryDatabase($"{Guid.NewGuid()}.db"));

            // Initialize the database with seed data and context accessors services
            var serviceProvider = services.BuildServiceProvider();
            var databaseContext = serviceProvider.GetRequiredService<OpenReddingDbContext>();
            databaseContext.Database.EnsureCreated();
            OpenReddingDatabaseInitializer.Initialize(databaseContext).Wait();

            Context = databaseContext;
            TestUri = new Uri("http://test-domain.com");
            TestEmployee = new Employee
            {
                EmployeeId = 1,
                FirstName = "Joey",
                MiddleName = "M",
                LastName = "Mckenzie",
                JobTitle = "Software Engineer",
                BasePay = 100m,
                Benefits = 10m,
                EmployeeAgency = EmployeeAgency.Redding,
                EmployeeStatus = EmployeeStatus.FullTime,
                Notes = "Great!",
                Year = 2020,
                OtherPay = 0m,
                OvertimePay = 0m,
                PensionDebt = 0m,
                TotalPay = 150m,
                TotalPayWithBenefits = 170m
            };

            TestCsvReadDto = new TransparentCaliforniaCsvReadEmployeeDto
            {
                EmployeeName = "John Smith",
                JobTitle = "Accountant",
                BasePay = 100m,
                OvertimePay = 10m,
                OtherPay = 20m,
                Benefits = 30m,
                TotalPay = 125m,
                PensionDebt = 10m,
                TotalPayWithBenefits = 150m,
                Year = 2019,
                Notes = string.Empty,
                EmployeeAgency = "Shasta County",
                EmployeeStatus = "FT"
            };
        }

        protected OpenReddingDbContext Context { get; }

        protected Uri TestUri { get; }

        protected Employee TestEmployee { get; }

        protected TransparentCaliforniaCsvReadEmployeeDto TestCsvReadDto { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Database.EnsureDeleted();
                Context.Dispose();
            }
        }
    }
}
