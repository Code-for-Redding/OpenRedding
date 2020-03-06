namespace OpenRedding.Infrastructure.Mappings
{
    using System;
    using Domain.Salaries.Dtos;
    using Domain.Salaries.Entities;

    public static class CustomConverters
    {
        public static Func<TransparentCaliforniaCsvReadEmployeeDto, string?> AgencyConverter => row =>
        {
            if (string.Equals(row.EmployeeAgency, "Shasta County", StringComparison.CurrentCultureIgnoreCase))
            {
                return nameof(EmployeeAgency.ShastaCounty);
            }

            return string.Equals(row.EmployeeAgency, "Redding", StringComparison.CurrentCultureIgnoreCase)
                ? nameof(EmployeeAgency.Redding)
                : nameof(EmployeeAgency.Other);
        };

        public static Func<TransparentCaliforniaCsvReadEmployeeDto, string?> StatusConverter => row =>
        {
            if (string.Equals(row.EmployeeStatus, "FT", StringComparison.CurrentCultureIgnoreCase))
            {
                return nameof(EmployeeStatus.FullTime);
            }

            return string.Equals(row.EmployeeStatus, "PT", StringComparison.CurrentCultureIgnoreCase)
                ? nameof(EmployeeStatus.PartTime)
                : nameof(EmployeeStatus.Other);
        };
    }
}