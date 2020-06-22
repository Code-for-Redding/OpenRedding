namespace OpenRedding.Core.Tests.Infrastructure
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using OpenRedding.Domain.Salaries.Entities;
    using OpenRedding.Domain.Salaries.Enums;
    using OpenRedding.Infrastructure.Persistence;
    using OpenRedding.Infrastructure.Persistence.Data;

    public class TestFixture : IDisposable
    {
        public TestFixture()
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
        }

        protected OpenReddingDbContext Context { get; }

        protected Uri TestUri { get; }

        protected Employee TestEmployee { get; }

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
