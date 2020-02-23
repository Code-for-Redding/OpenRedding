namespace OpenRedding.Data
{
    using System;
    using System.Threading.Tasks;
    using Domain.Salaries.Entities;

    public static class OpenReddingDbInitializer
    {
        public static async Task Initialize(OpenReddingDbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context), "Database context cannot be resolved, please check the service provider");
            }

            await SeedEmployees(context);
        }

        private static async Task SeedEmployees(OpenReddingDbContext context)
        {
            if (context.Employees is null)
            {
                throw new ArgumentNullException(nameof(context), "Database context does not contain a reference to employees DbSet");
            }

            var testEmployee1 = new Employee
            {
                EmployeeName = "John Smith",
                JobTitle = "Software Engineer",
                Year = 2020,
                BasePay = 100m,
                Benefits = 100m,
                PensionDebt = 100m,
                OtherPay = 100m,
                OvertimePay = 100m,
                TotalPay = 100m,
                TotalPayWithBenefits = 100m,
                Notes = "Great worker, the best",
                EmployeeAgency = EmployeeAgency.Redding,
                EmployeeStatus = EmployeeStatus.FullTime
            };

            var testEmployee2 = new Employee
            {
                EmployeeName = "Mary Smith",
                JobTitle = "Software Engineering Manager",
                Year = 2019,
                BasePay = 120m,
                Benefits = 100m,
                PensionDebt = 100m,
                OtherPay = 100m,
                OvertimePay = 100m,
                TotalPay = 100m,
                TotalPayWithBenefits = 170m,
                Notes = "Great worker, even better than John",
                EmployeeAgency = EmployeeAgency.Redding,
                EmployeeStatus = EmployeeStatus.FullTime
            };

            var testEmployee3 = new Employee
            {
                EmployeeName = "Joe Shmoe",
                JobTitle = "Accountant",
                Year = 2018,
                BasePay = 100m,
                Benefits = 100m,
                PensionDebt = 100m,
                OtherPay = 100m,
                OvertimePay = 100m,
                TotalPay = 100m,
                TotalPayWithBenefits = 100m,
                Notes = "Awesome",
                EmployeeAgency = EmployeeAgency.ShastaCounty,
                EmployeeStatus = EmployeeStatus.PartTime
            };

            var testEmployee4 = new Employee
            {
                EmployeeName = "Joey Mckenzie",
                JobTitle = "Senior Software Engineer",
                Year = 2020,
                BasePay = 95m,
                Benefits = 100m,
                PensionDebt = 100m,
                OtherPay = 100m,
                OvertimePay = 100m,
                TotalPay = 100m,
                TotalPayWithBenefits = 150m,
                Notes = "Amazing, loves .NET Core",
                EmployeeAgency = EmployeeAgency.ShastaCounty,
                EmployeeStatus = EmployeeStatus.PartTime
            };

            await context.Employees.AddRangeAsync(testEmployee1, testEmployee2, testEmployee3, testEmployee4);
            await context.SaveChangesAsync();
        }
    }
}